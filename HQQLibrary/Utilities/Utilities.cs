﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HQQLibrary.Utilities
{
    public class Utilities
    {
        public enum HMACCoding { SHA256, SHA512 };
        public static string HashHmac(HMACCoding encode, string message, string secret)
        {
            string result = string.Empty;
            Encoding encoding = Encoding.UTF8;

            switch (encode)
            {
                case HMACCoding.SHA256:

                    using (HMACSHA256 hmac = new HMACSHA256(encoding.GetBytes(secret)))
                    {
                        var msg = encoding.GetBytes(message);
                        var hash = hmac.ComputeHash(msg);
                        result = BitConverter.ToString(hash).ToLower().Replace("-", string.Empty);
                    }

                    break;
                case HMACCoding.SHA512:

                    using (HMACSHA512 hmac = new HMACSHA512(encoding.GetBytes(secret)))
                    {
                        var msg = encoding.GetBytes(message);
                        var hash = hmac.ComputeHash(msg);
                        result = BitConverter.ToString(hash).ToLower().Replace("-", string.Empty);
                    }

                    break;
            }

            return result;

        }
    }
}
