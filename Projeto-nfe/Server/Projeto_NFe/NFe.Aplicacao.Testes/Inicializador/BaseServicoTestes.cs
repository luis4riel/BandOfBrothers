using NFe.Aplicacao.Mapping;
using NUnit.Framework;

namespace NFe.Aplicacao.Testes.Inicializador
{
    [TestFixture]
    public class BaseServicoTestes
    {
        [OneTimeSetUp]
        public void InitializeOnceTime()
        {
            AutoMapperInitializer.Reset();
            AutoMapperInitializer.Initialize();
        }
    }
}