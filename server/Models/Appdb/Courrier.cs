using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArchiveCorr.Models.Appdb
{
  [Table("Courriers", Schema = "public")]
  public partial class Courrier
  {
    [Key]
    public Guid courid
    {
      get;
      set;
    }

    public ICollection<Document> Documents { get; set; }
    public DateTime DateArrivDepart
    {
      get;
      set;
    }
    public DateTime DateCorresp
    {
      get;
      set;
    }
    public int? Structurid
    {
      get;
      set;
    }
    public Structure Structure { get; set; }
    public int? Typeid
    {
      get;
      set;
    }
    public TypesCourrier TypesCourrier { get; set; }
    public int? Categorid
    {
      get;
      set;
    }
    public Category Category { get; set; }
    public int? Correspid
    {
      get;
      set;
    }
    public Correspondant Correspondant { get; set; }
    public string Objet
    {
      get;
      set;
    }
    public string num
    {
      get;
      set;
    }
    public string numInterne
    {
      get;
      set;
    }
  }
}
