﻿using Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User_API.Models
{
	public class User
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; } = "";
		public string Role { get; set; }
		
		public string NameEN { get; set; }
		public string NameJP { get; set; }
		public int Age { get; set; }
		public List<Pokemon>? Pokemons { get; set; }
	}
}
