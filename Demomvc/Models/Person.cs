using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;

namespace Demomvc.Models;
public class Person
{
    public int Id { get ; set;}
    public string? Title {get; set;}
    [DataType(DataType.Date)]
    public DateTime ReleaseDate {get; set;}
    public string? Genre {get; set;}
    public decimal price {get; set;}
}