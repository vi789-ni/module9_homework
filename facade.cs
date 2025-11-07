using System;

namespace FacadePattern
{
    class TV
    {
        public void On() => Console.WriteLine("Телевизор включен.");
        public void Off() => Console.WriteLine("Телевизор выключен.");
        public void SetChannel(int channel) => Console.WriteLine($"Выбран канал {channel}.");
    }

    class AudioSystem
    {
        private int volume = 5;
        public void On() => Console.WriteLine("Аудиосистема включена.");
        public void Off() => Console.WriteLine("Аудиосистема выключена.");
        public void SetVolume(int level)
        {
            volume = level;
            Console.WriteLine($"Громкость установлена на {volume}.");
        }
    }

    class DVDPlayer
    {
        public void Play() => Console.WriteLine("Воспроизведение DVD.");
        public void Pause() => Console.WriteLine("Пауза DVD.");
        public void Stop() => Console.WriteLine("Остановка DVD.");
    }

    class GameConsole
    {
        public void On() => Console.WriteLine("Игровая консоль включена.");
        public void StartGame(string game) => Console.WriteLine($"Запуск игры: {game}");
    }

    class HomeTheaterFacade
    {
        private TV tv;
        private AudioSystem audio;
        private DVDPlayer dvd;
        private GameConsole console;

        public HomeTheaterFacade(TV tv, AudioSystem audio, DVDPlayer dvd, GameConsole console)
        {
            this.tv = tv;
            this.audio = audio;
            this.dvd = dvd;
            this.console = console;
        }

        public void WatchMovie()
        {
            Console.WriteLine("\n Сценарий: Просмотр фильма ");
            tv.On();
            audio.On();
            audio.SetVolume(7);
            dvd.Play();
        }

        public void StopMovie()
        {
            Console.WriteLine("\n Остановка фильма ");
            dvd.Stop();
            audio.Off();
            tv.Off();
        }

        public void PlayGame(string game)
        {
            Console.WriteLine("\nСценарий: Игра ");
            console.On();
            audio.On();
            audio.SetVolume(6);
            tv.On();
            console.StartGame(game);
        }

        public void ListenMusic()
        {
            Console.WriteLine("\nСценарий: Прослушивание музыки ");
            tv.On();
            audio.On();
            audio.SetVolume(5);
            Console.WriteLine("Аудиовход на телевизоре установлен на аудиосистему.");
        }

        public void SetVolume(int level)
        {
            audio.SetVolume(level);
        }
    }

    class Program
    {
        static void Main()
        {
            TV tv = new TV();
            AudioSystem audio = new AudioSystem();
            DVDPlayer dvd = new DVDPlayer();
            GameConsole console = new GameConsole();

            HomeTheaterFacade homeTheater = new HomeTheaterFacade(tv, audio, dvd, console);

            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1 - Смотреть фильм");
                Console.WriteLine("2 - Остановить фильм");
                Console.WriteLine("3 - Играть в игру");
                Console.WriteLine("4 - Слушать музыку");
                Console.WriteLine("5 - Изменить громкость");
                Console.WriteLine("0 - Выход");
                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        homeTheater.WatchMovie();
                        break;
                    case "2":
                        homeTheater.StopMovie();
                        break;
                    case "3":
                        Console.Write("Введите название игры: ");
                        string game = Console.ReadLine();
                        homeTheater.PlayGame(game);
                        break;
                    case "4":
                        homeTheater.ListenMusic();
                        break;
                    case "5":
                        Console.Write("Введите уровень громкости (0-10): ");
                        if (int.TryParse(Console.ReadLine(), out int vol))
                            homeTheater.SetVolume(vol);
                        break;
                    case "0":
                        Console.WriteLine("Выход...");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }
    }
}
