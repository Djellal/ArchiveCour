using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArchiveCorr.Models.Appdb
{
  [Table("Correspondants", Schema = "public")]
  public partial class Correspondant
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }

    public ICollection<Courrier> Courriers { get; set; }
    public string Nom
    {
      get;
      set;
    }
    public string Tel
    {
      get;
      set;
    }
    public string Email
    {
      get;
      set;
    }
  }
}
