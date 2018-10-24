using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Aplicacao.Mapping
{
    public class AutoMapperInitializer
    {
        public static void Inicializador()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles(typeof(AutoMapperInitializer));
            });
        }
        public static void Reset() => Mapper.Reset();
    }
}
