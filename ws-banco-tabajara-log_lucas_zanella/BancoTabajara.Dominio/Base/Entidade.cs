using BancoTabajara.Infra.Validação;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Dominio.Base
{
    [ExcludeFromCodeCoverage]
    public class Entidade : IValidationEntity
    {
        public virtual int Id { get; set; }
        public override bool Equals(object obj)
        {
            var other = obj as Entidade;

            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (Id == 0 || other.Id == 0)
                return false;

            return Id == other.Id;
        }
        
        public static bool operator ==(Entidade a, Entidade b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }
        
        public static bool operator !=(Entidade a, Entidade b)
        {
            return !(a == b);
        }
        
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
       
        public virtual void Validar()
        {
            
        }
    }
}
