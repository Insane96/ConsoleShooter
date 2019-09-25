using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using ConsoleEngine;
using System.IO;
using System.Windows.Input;
using System.Xml.Serialization;
using OpenTK.Input;

namespace Shooter
{
    class Shooter
    {
        public static bool gameOver = false;
        public static bool win = false;

        public static int enemies = 0;

        static void Main()
        {
            Engine.Init(48, 32, false, "Console Shooter");
            Console.WriteLine("Starting Console Shooter ...");

            float fpsDisplayTime = 1f;
            Player player = new Player(
                name: "player",
                health: 100f,
                movementSpeed: 10f,
                shootSpeed: 0.33f,
                shootingPos: new Vector2(1, 0),
                size: new Vector2(3, 2),
                shape: new string[2]
                {
                        " ■ ",
                        "███",
                }
            );
            Engine.AddGameObject(player);

            Console.WriteLine("Beginning enemies initalization ...");
            if (Directory.Exists(Directory.GetCurrentDirectory() + @"\enemies"))
            {
                string[] fileList = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\enemies");
                if (fileList.Length > 0)
                {
                    Console.WriteLine("Directory exist and has files in. Reading them ...");
                    LoadEnemies();
                    Console.WriteLine("Successfully read enemies from files");
                }
                else
                {
                    Console.WriteLine("Directory exist but no files in, creating default enemies and loading them");
                    EnemySerializable enemy = new EnemySerializable(
                        health: 100f,
                        damage: 8f,
                        pos: new Vector2(Renderer.GetWindowWidth() / 2, Rand.GetRandomFloat(10, 15)),
                        shootingPos: new Vector2(1, 1),
                        movementSpeed: 4f,
                        maxMovementSpeed: 8f,
                        increaseMovementSpeed: 0.5f,
                        initialAttackSpeed: 2.5f,
                        maxAttackSpeed: 0.4f,
                        cooldownReduction: 0.4f,
                        size: new Vector2(3, 2),
                        shape: new string[2]
                        {
                            "███",
                            " V ",
                        }
                    );
                    XML.Serialize(typeof(EnemySerializable), enemy, Directory.GetCurrentDirectory() + @"\enemies\defaultEnemy1.xml");
                    enemy = new EnemySerializable(
                        health: 250f,
                        damage: 30f,
                        pos: new Vector2(Renderer.GetWindowWidth() / 2, Rand.GetRandomFloat(0, 5)),
                        shootingPos: new Vector2(2, 1),
                        movementSpeed: 2f,
                        maxMovementSpeed: 3f,
                        increaseMovementSpeed: 0.5f,
                        initialAttackSpeed: 1.25f,
                        maxAttackSpeed: 0.8f,
                        cooldownReduction: 0.25f,
                        new Vector2(5, 3),
                        shape: new string[3]
                        {
                            "█████",
                            "█ ■ █",
                            "█   █",
                        }
                    );
                    XML.Serialize(typeof(EnemySerializable), enemy, Directory.GetCurrentDirectory() + @"\enemies\defaultEnemy2.xml");

                    LoadEnemies();
                }
            }
            else
            {
                Console.WriteLine("Directory doesn't exist, creating it");
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\enemies");
            }

            Console.WriteLine("Startup completed!");
            Thread.Sleep(1500);
            Console.Clear();

            do
            {
                Engine.MainLoop();

                if (!gameOver && !win)
                {
                    if (player.isDead)
                        gameOver = true;
                }

                if (enemies == 0 && !win)
                {
                    win = true;
                }

                if (gameOver)
                    Renderer.Put("Game Over", new Vector2(Renderer.GetWindowWidth() / 2 - 4, Renderer.GetWindowHeight() / 2 - 1), ConsoleColor.Red);
                if (win)
                {
                    Renderer.Put("You won!", new Vector2(Renderer.GetWindowWidth() / 2 - 3, Renderer.GetWindowHeight() / 2 - 2), ConsoleColor.Green);
                }

                fpsDisplayTime -= Time.DeltaTime;
                if (fpsDisplayTime <= 0f)
                {
                    fpsDisplayTime = 0.5f;
                    Console.Title = "FPS: " + Math.Round(Engine.fps);
                }

            } while (!Input.IsKeyPressed(Key.Escape));
        }

        private static void LoadEnemies()
        {
            string[] fileList = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\enemies");
            EnemySerializable enemy;
            int enemyNumber = 0;
            foreach (string file in fileList)
            {
                enemy = (EnemySerializable)XML.Deserialize(typeof(EnemySerializable), file);
                Enemy newEnemy = new Enemy("enemy" + enemyNumber++, enemy);
                enemies++;
                Engine.AddGameObject(newEnemy);
            }
        }
    }
}
