using AppWeb.Helpers.Config;

namespace AppWeb.Modules.Core.Entities
{
    public partial class Parametro : Entity<Guid>
    {
        public string Empresa { get; set; }
        public string Nit { get; set; }
        public int? DefaultPageIndex { get; set; }
        public int? DefaultPageSize { get; set; }
        public DateTime CreateDateUtc { get; set; }
    }
}
