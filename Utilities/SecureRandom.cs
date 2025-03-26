using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Management;

public class SecureRandom
{
    private int seed;

    /// <summary>
    /// This is more secure.
    /// </summary>
    public SecureRandom()
    {
        // Generate a seed based on various factors including the machine's MAC address and CPU identifier
        var macBytes = GetMacAddressBytes();
        var cpuIdentifier = GetCpuIdentifier();
        var timeSeed = (int)DateTime.Now.Ticks;

        seed = CombineBytesToInt(macBytes) ^ cpuIdentifier.GetHashCode() ^ timeSeed;
    }

    /// <summary>
    /// This should only be used for testing.
    /// </summary>
    /// <param name="seed">The seed value to force.</param>
    public SecureRandom(int seed)
    {
        this.seed = seed;
    }

    // Generate a pseudorandom integer between minValue (inclusive) and maxValue (exclusive).
    public int Next(int minValue, int maxValue)
    {
        // Validate input parameters
        if (minValue >= maxValue)
            throw new ArgumentOutOfRangeException(nameof(minValue), "minValue must be less than maxValue");

        // Calculate the range of possible values
        var range = (long)maxValue - minValue;

        // Make sure the range fits within the limits of a positive integer
        if (range <= int.MaxValue)
        {
            // Use the linear congruential generator formula
            seed = (int)((214013 * (long)seed + 2531011) % 2147483647);
            return (int)((seed / 2147483647.0) * range) + minValue;
        }

        throw new ArgumentOutOfRangeException(nameof(maxValue), "The range is too large");
    }

    // Helper method to get MAC address bytes
    private byte[] GetMacAddressBytes()
    {
        var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        var firstInterface = networkInterfaces.FirstOrDefault();

        if (firstInterface != null)
        {
            var macBytes = firstInterface.GetPhysicalAddress().GetAddressBytes();
            return macBytes;
        }

        return Array.Empty<byte>();
    }

    // Helper method to get CPU identifier
    private string GetCpuIdentifier()
    {
        var cpuIdentifier = "";

        using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
        foreach (var obj in searcher.Get())
        {
            cpuIdentifier += obj["ProcessorId"].ToString();
        }

        return cpuIdentifier;
    }

    // Helper method to combine byte array into an integer
    private int CombineBytesToInt(byte[] bytes)
    {
        var result = 0;

        for (var i = 0; i < bytes.Length; i++)
        {
            result ^= bytes[i] << (i * 8);
        }

        return result;
    }
}
