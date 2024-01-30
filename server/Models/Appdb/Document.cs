using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArchiveCorr.Models.Appdb
{
  [Table("Documents", Schema = "public")]
  public partial class Document
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }
    public Guid? Courriid
    {
      get;
      set;
    }
    public Courrier Courrier { get; set; }
    public string Chemin
    {
      get;
      set;
    }
  }
}
