using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArchiveCorr.Models.Appdb
{
  [Table("Structures", Schema = "public")]
  public partial class Structure
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }

    public ICollection<Courrier> Courriers { get; set; }
    public string NomStructure
    {
      get;
      set;
    }
  }
}
