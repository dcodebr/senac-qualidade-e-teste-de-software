using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PokemonCardGame
{
    public partial class Form1 : Form
    {
        private List<Carta> meuDeck = new();
        private List<Carta> deckComputador = new();

        private Carta? minhaCartaAtiva;
        private Carta? cartaComputadorAtiva;

        private Random random = new();

        private Label lblMinhaCarta = new();
        private Label lblCartaComputador = new();
        private Label lblStatus = new();

        private Button btnAtaque = new();
        private Button btnProximaCarta = new();

        private ProgressBar barraMinhaVida = new();
        private ProgressBar barraVidaComputador = new();

        private int turno = 1;
        private bool minhaVez = true;

        public Form1()
        {
            InitializeComponent();
            CriarInterface();
            IniciarJogo();
        }

        private void CriarInterface()
        {
            Text = "Pokémon Card Battle - Exemplo";
            Width = 1000;
            Height = 700;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(35, 35, 45);

            // Título
            Label titulo = new Label
            {
                Text = "⚡ CARD BATTLE ⚡",
                ForeColor = Color.White,
                Font = new Font("Arial", 24, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(350, 20)
            };

            Controls.Add(titulo);

            // Área do computador
            Label lblComputador = new Label
            {
                Text = "COMPUTADOR",
                ForeColor = Color.LightBlue,
                Font = new Font("Arial", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(100, 100)
            };

            Controls.Add(lblComputador);

            lblCartaComputador = new Label
            {
                ForeColor = Color.White,
                BackColor = Color.DarkRed,
                Font = new Font("Arial", 18, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(300, 150),
                Location = new Point(100, 140),
                BorderStyle = BorderStyle.FixedSingle
            };

            Controls.Add(lblCartaComputador);

            barraVidaComputador = new ProgressBar
            {
                Minimum = 0,
                Maximum = 100,
                Value = 100,
                Size = new Size(300, 25),
                Location = new Point(100, 300)
            };

            Controls.Add(barraVidaComputador);

            // Área do jogador
            Label lblJogador = new Label
            {
                Text = "JOGADOR",
                ForeColor = Color.LightGreen,
                Font = new Font("Arial", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(600, 100)
            };

            Controls.Add(lblJogador);

            lblMinhaCarta = new Label
            {
                ForeColor = Color.White,
                BackColor = Color.DarkBlue,
                Font = new Font("Arial", 18, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(300, 150),
                Location = new Point(600, 140),
                BorderStyle = BorderStyle.FixedSingle
            };

            Controls.Add(lblMinhaCarta);

            barraMinhaVida = new ProgressBar
            {
                Minimum = 0,
                Maximum = 100,
                Value = 100,
                Size = new Size(300, 25),
                Location = new Point(600, 300)
            };

            Controls.Add(barraMinhaVida);

            // Status
            lblStatus = new Label
            {
                Text = "Turno 1",
                ForeColor = Color.White,
                Font = new Font("Arial", 14),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(600, 50),
                Location = new Point(200, 360)
            };

            Controls.Add(lblStatus);

            // Botão de ataque
            btnAtaque = new Button
            {
                Text = "⚔ ATACAR",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Size = new Size(200, 60),
                Location = new Point(250, 450),
                BackColor = Color.Firebrick,
                ForeColor = Color.White
            };

            btnAtaque.Click += BtnAtaque_Click;

            Controls.Add(btnAtaque);

            // Botão para trocar carta
            btnProximaCarta = new Button
            {
                Text = "🔄 PRÓXIMA CARTA",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Size = new Size(200, 60),
                Location = new Point(550, 450),
                BackColor = Color.DarkGreen,
                ForeColor = Color.White
            };

            btnProximaCarta.Click += BtnProximaCarta_Click;

            Controls.Add(btnProximaCarta);
        }

        private void IniciarJogo()
        {
            meuDeck = CriarDeckJogador();
            deckComputador = CriarDeckComputador();

            minhaCartaAtiva = meuDeck.First();
            cartaComputadorAtiva = deckComputador.First();

            AtualizarInterface();

            lblStatus.Text = "Sua vez! Escolha ATACAR.";
        }

        private List<Carta> CriarDeckJogador()
        {
            return new List<Carta>
            {
                new Carta("Pikachu", 100, 25),
                new Carta("Bulbasaur", 110, 20),
                new Carta("Charmander", 90, 30),
                new Carta("Squirtle", 120, 18)
            };
        }

        private List<Carta> CriarDeckComputador()
        {
            return new List<Carta>
            {
                new Carta("Eevee", 100, 22),
                new Carta("Psyduck", 115, 20),
                new Carta("Vulpix", 90, 28),
                new Carta("Meowth", 105, 24)
            };
        }

        private void BtnAtaque_Click(object? sender, EventArgs e)
        {
            if (!minhaVez)
            {
                MessageBox.Show("Aguarde o computador jogar!");
                return;
            }

            if (minhaCartaAtiva == null ||
                cartaComputadorAtiva == null)
                return;

            // Jogador ataca
            int dano = minhaCartaAtiva.Ataque;

            cartaComputadorAtiva.HP -= dano;

            if (cartaComputadorAtiva.HP < 0)
                cartaComputadorAtiva.HP = 0;

            lblStatus.Text =
                $"{minhaCartaAtiva.Nome} causou {dano} de dano!";

            AtualizarInterface();

            // Verifica se derrotou o computador
            if (cartaComputadorAtiva.HP <= 0)
            {
                DerrotarCartaComputador();
                return;
            }

            // Passa o turno para o computador
            minhaVez = false;

            Timer timer = new Timer();
            timer.Interval = 1000;

            timer.Tick += (s, args) =>
            {
                timer.Stop();
                TurnoComputador();
            };

            timer.Start();
        }

        private void TurnoComputador()
        {
            if (minhaCartaAtiva == null ||
                cartaComputadorAtiva == null)
                return;

            int dano = cartaComputadorAtiva.Ataque;

            minhaCartaAtiva.HP -= dano;

            if (minhaCartaAtiva.HP < 0)
                minhaCartaAtiva.HP = 0;

            lblStatus.Text =
                $"{cartaComputadorAtiva.Nome} atacou e causou {dano} de dano!";

            AtualizarInterface();

            if (minhaCartaAtiva.HP <= 0)
            {
                DerrotarMinhaCarta();
                return;
            }

            turno++;
            minhaVez = true;

            lblStatus.Text =
                $"Turno {turno}: sua vez!";
        }

        private void DerrotarCartaComputador()
        {
            lblStatus.Text =
                $"Você derrotou {cartaComputadorAtiva!.Nome}!";

            deckComputador.Remove(cartaComputadorAtiva);

            if (deckComputador.Count == 0)
            {
                MessageBox.Show(
                    "🎉 VOCÊ VENCEU!\n\nTodas as cartas do computador foram derrotadas.");

                btnAtaque.Enabled = false;
                btnProximaCarta.Enabled = false;
                return;
            }

            cartaComputadorAtiva = deckComputador.First();

            AtualizarInterface();

            minhaVez = true;
            turno++;

            lblStatus.Text =
                $"Nova carta do computador! Turno {turno}.";
        }

        private void DerrotarMinhaCarta()
        {
            lblStatus.Text =
                $"Sua carta {minhaCartaAtiva!.Nome} foi derrotada!";

            meuDeck.Remove(minhaCartaAtiva);

            if (meuDeck.Count == 0)
            {
                MessageBox.Show(
                    "💀 GAME OVER!\n\nTodas as suas cartas foram derrotadas.");

                btnAtaque.Enabled = false;
                btnProximaCarta.Enabled = false;
                return;
            }

            minhaCartaAtiva = meuDeck.First();

            AtualizarInterface();

            minhaVez = true;
            turno++;

            lblStatus.Text =
                $"Sua nova carta é {minhaCartaAtiva.Nome}.";
        }

        private void BtnProximaCarta_Click(object? sender, EventArgs e)
        {
            if (meuDeck.Count <= 1)
            {
                MessageBox.Show("Você não possui outra carta disponível.");
                return;
            }

            int indiceAtual = meuDeck.IndexOf(minhaCartaAtiva!);

            int proximoIndice = (indiceAtual + 1) % meuDeck.Count;

            minhaCartaAtiva = meuDeck[proximoIndice];

            lblStatus.Text =
                $"Você escolheu {minhaCartaAtiva.Nome}.";

            AtualizarInterface();
        }

        private void AtualizarInterface()
        {
            if (minhaCartaAtiva != null)
            {
                lblMinhaCarta.Text =
                    $"⚡ {minhaCartaAtiva.Nome}\n\n" +
                    $"HP: {minhaCartaAtiva.HP}\n" +
                    $"Ataque: {minhaCartaAtiva.Ataque}";

                barraMinhaVida.Value =
                    Math.Max(0, Math.Min(100, minhaCartaAtiva.HP));
            }

            if (cartaComputadorAtiva != null)
            {
                lblCartaComputador.Text =
                    $"🔥 {cartaComputadorAtiva.Nome}\n\n" +
                    $"HP: {cartaComputadorAtiva.HP}\n" +
                    $"Ataque: {cartaComputadorAtiva.Ataque}";

                barraVidaComputador.Value =
                    Math.Max(0, Math.Min(100, cartaComputadorAtiva.HP));
            }
        }
    }

    public class Carta
    {
        public string Nome { get; set; }

        public int HP { get; set; }

        public int Ataque { get; set; }

        public Carta(string nome, int hp, int ataque)
        {
            Nome = nome;
            HP = hp;
            Ataque = ataque;
        }
    }
}