using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;

namespace contenido
{
    internal class Utilidades
    {
        // Método auxiliar para levantar de forma rápida los usuarios del JSON cada vez que se necesita validar
        private static List<Usuario> ObtenerUsuariosPersistidos()
        {
            string path = "usuarios.json";
            if (File.Exists(path))
            {
                try
                {
                    string json = File.ReadAllText(path);
                    Biblioteca bibCargada = JsonSerializer.Deserialize<Biblioteca>(json);
                    if (bibCargada != null && bibCargada.usuarios != null)
                    {
                        return bibCargada.usuarios;
                    }
                }
                catch { }
            }
            return new List<Usuario>();
        }

        public static bool Iniciar_Sesion(ref Usuario usuario_activo)
        {
            Program.info = new string[2];
            while (true)
            {
                Program.cadena = Utilidades.menu(new string[] { "Ingresar datos", "Atras" });
                switch (Program.cadena)
                {
                    case "Ingresar datos":
                        {
                            Program.info = formulario(new string[] { "Ingrese su nombre de usuario: ", "Ingrese su contraseña: " });

                            // CORREGIDO: Lee directamente los usuarios reales guardados en el archivo JSON
                            List<Usuario> listaUsuarios = ObtenerUsuariosPersistidos();

                            foreach (Usuario us in listaUsuarios)
                            {
                                if (us.nombre_usuario == Program.info[0] && us.Clave_usuario == Program.info[1])
                                {
                                    usuario_activo = us;
                                    return true;
                                }
                            }
                        }
                        Console.WriteLine("Contraseña o nombre de usuario incorrectos");
                        Console.ReadKey();
                        break;
                    case "Atras":
                        return false;
                }
            }
        }

        public static bool crear_usuario(ref Usuario usuario_activo)
        {
            estado_usuario estado_previo = estado_usuario.inexistente;
            DateTime fecha_ingresada;
            Program.info = new string[3];
            while (true)
            {
                Program.cadena = Utilidades.menu(new string[] { "Ingresar datos", "Atras" });
                switch (Program.cadena)
                {
                    case "Ingresar datos":
                        {
                            Program.info = formulario(new string[] { "Ingrese su nombre de usuario: ", "Ingrese su fecha de nacimiento(año/mes/dia): ", "Ingrese su contraseña: " });
                            Program.cadena = Utilidades.menu(new string[] { "Cuenta privada", "Cuenta publica" });
                            switch (Program.cadena)
                            {
                                case "Cuenta privada":
                                    estado_previo = estado_usuario.privado;
                                    break;
                                case "Cuenta publica":
                                    estado_previo = estado_usuario.publico;
                                    break;
                            }

                            while (!DateTime.TryParse(Program.info[1], out fecha_ingresada))
                            {
                                Console.Write("Fecha no valida, reingrese su fecha de nacimiento(año/mes/dia): ");
                                Program.info[1] = Console.ReadLine();
                            }

                            bool existe_usuario = false;
                            List<Usuario> listaUsuarios = ObtenerUsuariosPersistidos();

                            foreach (Usuario us in listaUsuarios)
                            {
                                if (us.nombre_usuario == Program.info[0])
                                {
                                    Console.WriteLine("El nombre ingresado ya existe");
                                    Console.ReadKey();
                                    existe_usuario = true;
                                    break;
                                }
                            }

                            if (!existe_usuario)
                            {
                                Usuario usuario_creado = new Usuario(Program.info[0], Program.info[2], fecha_ingresada, estado_previo);

                                // CORREGIDO PASO CRÍTICO: Impacta el usuario en el archivo JSON físico de forma permanente
                                GestionUsuarios.GuardarNuevoUsuario(usuario_creado);

                                usuario_activo = usuario_creado;
                                Console.WriteLine("Usuario creado y guardado con éxito.");
                                Console.ReadKey();
                                return true;
                            }
                            break;
                        }
                    case "Atras":
                        return false;
                }
            }
        }

        public static string[] formulario(string[] info)
        {
            string[] resultados = new string[info.Length];
            for (int i = 0; i < info.Length; i++)
            {
                Console.Write(info[i]);
                resultados[i] = Console.ReadLine();
            }
            return resultados;
        }

        public static string menu(string[] opciones)
        {
            int indiceSeleccionado = 0;
            ConsoleKeyInfo tecla;
            Console.CursorVisible = false;
            do
            {
                Console.Clear();
                for (int i = 0; i < opciones.Length; i++)
                {
                    if (i == indiceSeleccionado)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ResetColor();
                    }
                    Console.WriteLine(opciones[i]);
                }
                tecla = Console.ReadKey(true);
                if (tecla.Key == ConsoleKey.UpArrow)
                {
                    indiceSeleccionado--;
                    if (indiceSeleccionado < 0) indiceSeleccionado = opciones.Length - 1;
                }
                else if (tecla.Key == ConsoleKey.DownArrow)
                {
                    indiceSeleccionado++;
                    if (indiceSeleccionado >= opciones.Length) indiceSeleccionado = 0;
                }
            } while (tecla.Key != ConsoleKey.Enter);
            Console.ResetColor();
            return opciones[indiceSeleccionado];
        }

        public static void menu_principal()
        {
            bool sesion_iniciada = false;
            Usuario usuario_activo = new Usuario();
            while (Program.continuar)
            {
                if (!sesion_iniciada)
                {
                    // CORREGIDO: Ortografía unificada de "Iniciar Sesion" para evitar fallas en el switch
                    Program.cadena = Utilidades.menu(new String[] { "Iniciar Sesion", "Crear Usuario", "Salir" });
                    switch (Program.cadena)
                    {
                        case "Iniciar Sesion":
                            sesion_iniciada = Utilidades.Iniciar_Sesion(ref usuario_activo);
                            break;
                        case "Crear Usuario":
                            sesion_iniciada = Utilidades.crear_usuario(ref usuario_activo);
                            break;
                        case "Salir":
                            Program.continuar = false;
                            break;
                    }
                }
                else
                {
                    Program.cadena = Utilidades.menu(new String[] { "Buscar pelicula", "Buscar serie", "Buscar usuario", "Ver datos de usuario", "Ver publicaciones", "Adivinar pelicula", "Cerrar sesion" });
                    switch (Program.cadena)
                    {
                        case "Buscar pelicula":
                            Utilidades.Buscar_contenido("pelicula", usuario_activo).Wait();
                            Program.continuar = true;
                            break;
                        case "Buscar serie":
                            Utilidades.Buscar_contenido("serie", usuario_activo).Wait();
                            Program.continuar = true;
                            break;
                        case "Buscar usuario":
                            Utilidades.Buscar_usuario();
                            Program.continuar = true;
                            break;
                        case "Ver publicaciones":
                            // Se espera código
                            break;
                        case "Adivinar pelicula":
                            // CORREGIDO: Se llama usando .Wait() para respetar el contexto asincrónico del juego
                            AhorcadoPeliculas.Jugar().Wait();
                            break;
                        case "Cerrar sesion": // CORREGIDO: "sesion" con 's' emparejado con la opción visual
                            Console.WriteLine("Se ha cerrado sesion. Presione una tecla para continuar...");
                            Console.ReadKey();
                            sesion_iniciada = false;
                            break;
                    }
                }
            }
        }

        public static async Task DescargarGeneros()
        {
            try
            {
                httpclient client = new httpclient();
                string url = $"https://api.themoviedb.org/3/genre/movie/list?api_key={Program.apiKey}&language=es-ES";
                string json = await Program.client.GetStringAsync(url);
                TotalGeneros respuestaGeneros = JsonSerializer.Deserialize<TotalGeneros>(json);

                if (respuestaGeneros != null && respuestaGeneros.genero != null)
                {
                    Program.ListaGeneros.Clear();
                    foreach (Genero gen in respuestaGeneros.genero)
                    {
                        Program.ListaGeneros.Add(gen.id, gen.name);
                    }
                }
                else
                {
                    Console.WriteLine("No se recibieron géneros.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error descargando géneros: {ex.Message}");
            }
        }

        public static async Task Buscar_contenido(string tipo, Usuario usuario_activo)
        {
            Program.info = new string[1];

            while (Program.continuar)
            {
                int resultadoActual = 0;
                int paginaActual = 1;

                Program.cadena = Utilidades.menu(new string[]
                {$"Buscar {tipo} solo por nombre", $"Buscar {tipo} con filtros", "Atras"});

                if (Program.cadena == "Atras")
                {
                    Program.continuar = false;
                    break;
                }

                Program.info = Utilidades.formulario(
                    new string[] { $"Ingrese el nombre de la {tipo} que desea buscar:" });

                if (string.IsNullOrWhiteSpace(Program.info[0]))
                    continue;

                string url_contenido = "";

                if (tipo == "pelicula")
                {
                    url_contenido =
                        $"https://api.themoviedb.org/3/search/movie?query={Uri.EscapeDataString(Program.info[0])}&language=es-ES&page=1&api_key={Program.apiKey}";
                }
                else if (tipo == "serie")
                {
                    url_contenido =
                        $"https://api.themoviedb.org/3/search/tv?query={Uri.EscapeDataString(Program.info[0])}&language=es-ES&page=1&api_key={Program.apiKey}";
                }

                try
                {
                    string json = await Program.client.GetStringAsync(url_contenido);
                    TotalContenidos respuesta =
                        JsonSerializer.Deserialize<TotalContenidos>(json);

                    if (respuesta == null ||
                        respuesta.results == null ||
                        respuesta.total_results == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("No se encontraron resultados");
                        Console.WriteLine("\nPresione una tecla para continuar");
                        Console.ReadKey(true);
                        continue;
                    }

                    while (true)
                    {
                        int indicePagina = resultadoActual % 20;

                        Console.Clear();

                        Console.WriteLine($"Resultado {resultadoActual + 1} de {respuesta.total_results}");
                        Console.WriteLine(new string('-', 50));

                        respuesta.MostrarDatos(indicePagina, usuario_activo);

                        Console.WriteLine();
                        Console.WriteLine("[<-] Anterior | [->] Siguiente | [↓] Opciones | [ESC] Salir");
                        ConsoleKey tecla = Console.ReadKey(true).Key;

                        if (tecla == ConsoleKey.Escape)
                        {
                            break;
                        }

                        if (tecla == ConsoleKey.DownArrow)
                        {
                            Program.cadena = Utilidades.menu(new string[]
                            {
                                "Crear publicacion",
                                "Salir"
                            });

                            switch (Program.cadena)
                            {
                                case "Crear publicacion":
                                    
                                    break;

                                case "Salir":
                                    break;
                            }

                            // Redibuja la película/serie actual al volver del menú
                            continue;
                        }

                        if (tecla == ConsoleKey.RightArrow)
                        {
                            resultadoActual++;

                            if (resultadoActual >= respuesta.total_results)
                            {
                                resultadoActual = 0;
                            }
                        }
                        else if (tecla == ConsoleKey.LeftArrow)
                        {
                            resultadoActual--;

                            if (resultadoActual < 0)
                            {
                                resultadoActual = respuesta.total_results - 1;
                            }
                        }
                        else
                        {
                            continue;
                        }

                        int paginaNecesaria = (resultadoActual / 20) + 1;

                        if (paginaNecesaria != paginaActual)
                        {
                            paginaActual = paginaNecesaria;

                            string nuevaUrl;

                            if (tipo == "pelicula")
                            {
                                nuevaUrl =
                                    $"https://api.themoviedb.org/3/search/movie?query={Uri.EscapeDataString(Program.info[0])}&language=es-ES&page={paginaActual}&api_key={Program.apiKey}";
                            }
                            else
                            {
                                nuevaUrl =
                                    $"https://api.themoviedb.org/3/search/tv?query={Uri.EscapeDataString(Program.info[0])}&language=es-ES&page={paginaActual}&api_key={Program.apiKey}";
                            }

                            json = await Program.client.GetStringAsync(nuevaUrl);

                            respuesta = JsonSerializer.Deserialize<TotalContenidos>(json);

                            if (respuesta == null ||
                                respuesta.results == null ||
                                respuesta.results.Count == 0)
                            {
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine($"Error al conectar con el servicio:");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("\nPresione una tecla para continuar...");
                    Console.ReadKey(true);
                }
            }

            Program.continuar = true;
        }
        public static void Buscar_usuario()
        {
            Program.info = new string[1];
            while (Program.continuar)
            {
                bool usuario_enc = false;
                Program.info = Utilidades.formulario(new String[] { $"Ingrese el nombre del usuario que desea buscar: " });

                // CORREGIDO: Busca sobre la lista persistida en el JSON real
                List<Usuario> listaUsuarios = ObtenerUsuariosPersistidos();

                foreach (Usuario us in listaUsuarios)
                {
                    if (us.nombre_usuario == Program.info[0] && us.estado == estado_usuario.publico)
                    {
                        usuario_enc = true;
                        us.MostrarDatosUsuario();
                    }
                }

                if (!usuario_enc)
                {
                    Console.WriteLine("No se encontro ningun usuario público con ese nombre.");
                    Console.ReadKey();
                }

                Program.cadena = Utilidades.menu(new String[] { "Atras" });
                if (Program.cadena == "Atras")
                {
                    Program.continuar = false;
                }
            }
        }
    }
}
