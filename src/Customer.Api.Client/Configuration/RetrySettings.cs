// -------------------------------------------------------------------------------------
//  <copyright file="RetrySettings.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Customer.Api.Client.Configuration;

/// <summary>
/// Defines the settings for retrying.
/// </summary>
public class RetrySettings
{
    /// <summary>
    /// Gets or sets a value indicatig whether this is enabled. 
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets a value indicatig whether jitter is enabled. 
    /// </summary>
    public bool JitterEnabled { get; set; }

    /// <summary>
    /// Gets or sets the Max jitter allowed in miliseconds.
    /// </summary>
    public int MaxJitterInMiliseconds { get; set; }

    /// <summary>
    /// Gets or sets the number of retries.
    /// </summary>
    public int NumberOfRetries { get; set; }

    /// <summary>
    /// Gets or sets backoff mechanism. 
    /// </summary>
    public RetriesBackoffSettings RetriesBackoff { get; set; }

    /// <summary>
    /// Gets or sets the sleep duration in miliseconds
    /// </summary>
    public int SleepDurationInMiliseconds { get; set; }

    /// <summary>
    /// Gets the sleep duration in miliseconds.
    /// It will use the configured backoff mechanism: constant, linear or exponential.
    /// If enabled, it will also add some jitter (random) to the sleep duration.
    /// </summary>
    /// <param name="retryAttempt">The retry attempt.</param>
    /// <returns>The sleep duration in miliseconds.</returns>
    public TimeSpan GetSleepDuration(int retryAttempt)
    {
        TimeSpan value = TimeSpan.Zero;

        switch (RetriesBackoff) 
        {
            case RetriesBackoffSettings.Constant:
                value = TimeSpan.FromMilliseconds(SleepDurationInMiliseconds);
                break;

            case RetriesBackoffSettings.Linear:
                value = TimeSpan.FromMilliseconds(SleepDurationInMiliseconds * retryAttempt);
                break;

            case RetriesBackoffSettings.Exponential:
                value = TimeSpan.FromMilliseconds(Math.Pow(SleepDurationInMiliseconds, retryAttempt));
                break;

            default:
                break;
        }

        if (!JitterEnabled)
        {
            return value;
        }

        var jitterer = new Random();

        return value + TimeSpan.FromMilliseconds(jitterer.Next(0, MaxJitterInMiliseconds));
    }
}