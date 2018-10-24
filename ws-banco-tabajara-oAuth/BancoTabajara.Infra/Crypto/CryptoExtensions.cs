﻿using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BancoTabajara.Infra.Crypto
{
	[ExcludeFromCodeCoverage]
	public static class CryptoExtensions
	{
		public static string GenerateHash(this string passwordToHash)
		{
			string hashed = Convert.ToBase64String( KeyDerivation.Pbkdf2(
				password: passwordToHash,
				salt: new byte[ 0 ],
				prf: KeyDerivationPrf.HMACSHA1,
				iterationCount: 10000,
				numBytesRequested: 256 / 8 ) );

			return hashed;
		}
	}
}
