using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace contenido
{
    internal static class AhorcadoPeliculas
    {
        public static async Task Jugar()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Cargando juego, por favor espere...");

                // 1. Reutilizamos el cliente y api_key globales de Program para traer pelis populares
                string urlPopulares = $"https://api.themoviedb.org/3/movie/popular?api_key={Program.apiKey}&language=es-ES";
                string jsonPopulares = await Program.client.GetStringAsync(urlPopulares);

                // REUTILIZADO: Usamos la clase TotalContenidos que ya maneja tu programa nuevo
                TotalContenidos populares = JsonSerializer.Deserialize<TotalContenidos>(jsonPopulares);

                if (populares == null || populares.results == null || populares.results.Count == 0)
                {
                    Console.WriteLine("No se pudieron cargar películas para el juego.");
                    Console.ReadKey();
                    return;
                }

                Random rand = new Random();
                // Ahora lee directo los objetos de tipo 'Contenido' que metimos en TotalContenidos
                Contenido peliculaElegida = populares.results[rand.Next(populares.results.Count)];

                // 2. Reutilizado: Buscamos el nombre del género directamente desde el diccionario estático de Program
                string nombreGenero = "Desconocido";

                // CORREGIDO: Se usa .Length porque genre_ids en Contenido.cs es un arreglo (int[])
                if (peliculaElegida.genre_ids != null && peliculaElegida.genre_ids.Length > 0)
                {
                    int idGenero = peliculaElegida.genre_ids[0];
                    if (Program.ListaGeneros != null && Program.ListaGeneros.ContainsKey(idGenero))
                    {
                        nombreGenero = Program.ListaGeneros[idGenero];
                    }
                }

                // 3. Traer actor principal (Mantenemos la petición específica de créditos)
                string urlCreditos = $"https://api.themoviedb.org/3/movie/{peliculaElegida.id}/credits?api_key={Program.apiKey}&language=es-ES";
                string jsonCreditos = await Program.client.GetStringAsync(urlCreditos);
                CreditosJuego creditos = JsonSerializer.Deserialize<CreditosJuego>(jsonCreditos);

                string actorYPersonaje = "Desconocido";
                if (creditos != null && creditos.cast != null && creditos.cast.Count > 0)
                {
                    CastMemberJuego actorPrincipal = creditos.cast[0];
                    actorYPersonaje = $"{actorPrincipal.name} como {actorPrincipal.character}";
                }

                // 4. Armar las pistas de forma segura
                string sinopsis = peliculaElegida.overview ?? "Sin sinopsis disponible.";
                string overviewRecortada = sinopsis.Length > 70 ? sinopsis.Substring(0, 70) + "..." : sinopsis;

                // CORREGIDO: Extraemos el año de estreno usando DateTime de forma prolija
                string anioLanzamiento = "Desconocido";
                if (DateTime.TryParse(peliculaElegida.release_date, out DateTime fechaEstreno))
                {
                    anioLanzamiento = fechaEstreno.Year.ToString();
                }

                string[] pistas = {
                    $"Sinopsis: {overviewRecortada}",
                    $"Género: {nombreGenero}",
                    $"Actor principal: {actorYPersonaje}",
                    $"Año de lanzamiento: {anioLanzamiento}" // Agregamos el año como pista extra
                };

                // 5. JUEGO DEL AHORCADO
                // Como es una película de la API, usamos .title (o .name si fuera serie)
                string tituloOriginal = !string.IsNullOrEmpty(peliculaElegida.title) ? peliculaElegida.title : peliculaElegida.name;

                if (string.IsNullOrEmpty(tituloOriginal)) tituloOriginal = "ERROR";

                string palabra = tituloOriginal.ToUpper();
                char[] letras = palabra.ToCharArray();
                int longitud = letras.Length;
                int[] adivinadas = new int[longitud];
                int intentos = 4;
                int contadorPistas = 0;
                bool juegoActivo = true;

                Console.Clear();

                do
                {
                    Console.WriteLine("        AHORCADO DE PELÍCULAS           ");
                 
                    int letrasEncontradas = 0;

                    for (int h = 0; h < longitud; h++)
                    {
                        // Revelar caracteres especiales comunes de forma automática para que no sea imposible
                        if (adivinadas[h] == 1)
                        {
                            Console.Write(letras[h] + " ");
                            letrasEncontradas++;
                        }
                        else if (letras[h] == ' ')
                        {
                            Console.Write("   ");
                            letrasEncontradas++;
                        }
                        else if (char.IsPunctuation(letras[h]) || char.IsSymbol(letras[h]))
                        {
                            Console.Write(letras[h] + " ");
                            letrasEncontradas++;
                        }
                        else
                        {
                            Console.Write("_ ");
                        }
                    }
                    Console.WriteLine("\n");

                    if (letrasEncontradas == longitud)
                    {
                        juegoActivo = false;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"¡Felicitaciones! Ganaste. La película era: {tituloOriginal}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"Vidas restantes: {intentos}");
                        Console.WriteLine("----------------------------------------");

                        // Mostrar pistas acumuladas
                        for (int i = 0; i <= contadorPistas && i < pistas.Length; i++)
                        {
                            Console.WriteLine($"* Pista {i + 1}: {pistas[i]}");
                        }
                        Console.WriteLine("----------------------------------------");

                        Console.Write("Ingrese una letra: ");
                        string entrada = Console.ReadLine();
                        Console.Clear();

                        if (string.IsNullOrEmpty(entrada)) continue;

                        char letra = char.ToUpper(entrada[0]);
                        bool acerto = false;

                        for (int i = 0; i < longitud; i++)
                        {
                            if (letras[i] == letra && adivinadas[i] != 1)
                            {
                                adivinadas[i] = 1;
                                acerto = true;
                            }
                        }

                        if (!acerto)
                        {
                            intentos--;
                            // CORREGIDO: Se cambió .Count por .Length porque genre_ids es un arreglo (int[])
                            if (peliculaElegida.genre_ids != null && peliculaElegida.genre_ids.Length > 0)
                            {
                                contadorPistas++;
                            }
                        }

                        if (intentos == 0)
                        {
                            juegoActivo = false;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"GAME OVER. Te quedaste sin vidas.");
                            Console.WriteLine($"La película oculta era: {tituloOriginal}");
                            Console.ResetColor();
                        }
                    }

                } while (juegoActivo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo ejecutar el juego: {ex.Message}");
            }

            Console.WriteLine("\nPresione una tecla para volver al menú anterior...");
            Console.ReadKey();
        }
    }

    // Clases auxiliares para parsear los créditos de la API (exclusivas del juego)
    public class CreditosJuego
    {
        public List<CastMemberJuego> cast { get; set; }
    }

    public class CastMemberJuego
    {
        public string name { get; set; }
        public string character { get; set; }
    }
}
