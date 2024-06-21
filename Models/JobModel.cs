using System.ComponentModel.DataAnnotations;

namespace IsTakip.Models;

public class JobModel
{
    public int Id { get; set; }
    public string Job { get; set; }
    public string Detail { get; set; }
    public string Durum { get; set; }
    public string AtananKisi { get; set; }
    public DateTime CreatedTime { get; set; }
    
    
}