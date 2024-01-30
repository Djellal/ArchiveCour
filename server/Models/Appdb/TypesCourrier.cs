using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArchiveCorr.Models.Appdb
{
  [Table("TypesCourriers", Schema = "public")]
  public partial class TypesCourrier
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }

    public ICollection<Courrier> Courriers { get; set; }
    public string NomType
    {
      get;
      set;
    }
  }
}
