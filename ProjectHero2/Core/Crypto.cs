/**
 *   _____           _                _____                
 *  |  _  |___ ___  |_|___ ___| |_   |  |  |___ ___ ___ 
 *  |   __|  _| . | | | -_|  _|  _|  |     | -_|  _| . |
 *  |__|  |_| |___|_| |___|___|_|    |__|__|___|_| |___|
 *                |___|      
 *    
 * Copyright © 2017 Alphonso Turner
 * All Rights Reserved
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core
{
    public static class Crypto
    {
        public static string GenerateMD5Hash(string input)
        {
            // ================================================================================
            // Grab the current unicode encoder.
            // ================================================================================
            Encoder unicode16Encoder = Encoding.Unicode.GetEncoder();

            // ================================================================================
            // Create a buffer to hold the string in its byte representation.
            // ================================================================================
            byte[] unicodeText = new byte[input.Length * 2];
            unicode16Encoder.GetBytes(input.ToCharArray(), 0, input.Length, unicodeText, 0, true);

            // ================================================================================
            // Now let's hash it using the MD5CryptoServiceProvider.
            // ================================================================================
            MD5 md5Hash = new MD5CryptoServiceProvider();
            byte[] result = md5Hash.ComputeHash(unicodeText);

            // ================================================================================
            // Convert the hash into hex now.
            // ================================================================================
            StringBuilder hashBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                hashBuilder.Append(result[i].ToString("X2"));
            }

            return hashBuilder.ToString();
        }
    }
}
