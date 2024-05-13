namespace TodoApi.src.Entity;

using System.ComponentModel.DataAnnotations;

public class Todo
{
    [Required(ErrorMessage = "El Pokemon necesita una ID para asignar en la pokedex.")]
    public int Id { get; set;}

    [Required(ErrorMessage = "El nombre del pokemon es necesario.")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "El tipo del pokemon es necesario.")]
    public string? Tipo { get; set; }

    [Range(0,40, ErrorMessage = "Valor del ataque 1 fuera de rango (0-40)")]
    public int Atk1 { get; set; }
    
    [Range(0,40, ErrorMessage = "Valor del ataque 2 fuera de rango (0-40)")]
    public int Atk2 { get; set; }

    [Range(0,40, ErrorMessage = "Valor del ataque 3 fuera de rango (0-40)")]
    public int Atk3 { get; set; }

    [Range(0,40, ErrorMessage = "Valor del ataque 4 fuera de rango (0-40)")]
    public int Atk4 { get; set; }

    [Range(1,30, ErrorMessage = "Valor de la defensa fuera de rango (1-30)")]
    public int Def { get; set; }

}
