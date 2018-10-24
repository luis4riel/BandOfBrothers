using Effort.Provider;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.Data.Testes.Initializer
{
    [TestFixture]
    public class EffortTestBase
    {
        [OneTimeSetUp]
        public void InitializeOneTime()
        {
            EffortProviderConfiguration.RegisterProvider();
        }

        [SetUp]
        public void Initialize()
        {
            EffortProviderFactory.ResetDb();
        }
    }
}
