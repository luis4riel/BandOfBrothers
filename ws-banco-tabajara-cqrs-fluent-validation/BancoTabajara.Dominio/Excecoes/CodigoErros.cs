﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Dominio.Excecoes
{
    public enum CodigoErros
    {
        Unauthorized = 401,
        Forbidden = 0403,       
        NotFound = 0404,
        AlreadyExists = 0409,
        NotAllowed = 0405,
        InvalidObject = 0422,
        Unhandled = 0500,
        ServiceUnavailable = 0503
    }
}
