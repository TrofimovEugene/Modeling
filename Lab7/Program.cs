using ConsoleTables;
using System;

namespace Lab7
{
	public class Program
	{
		private static double Q;
		private static double P_otk;
		private static double P_och;
		private static double _n;
		private static double _m;
		private static double _N;
		private static double t_och;
		private static double t_s;
		private static double k_n;

		private static string[,] dataRes = new string[3,7];
		public static void Main(string[] args)
		{
			var lam = 2.6;
			var mu = 1;
			var n = 3;
			var m = 2;
			Console.WriteLine("\nРассчитываем случай M/M/n/0: \n");
			M_M_n_0(lam, mu, n, m);
			Console.WriteLine("\nРассчитываем случай M/M/n/inf: \n");
			M_M_n_inf(lam, mu, n, m);
			Console.WriteLine("\nРассчитываем случай M/M/n/m: \n");
			M_M_n_m(lam, mu, n, m);
			Console.WriteLine();

			var table = new ConsoleTable("Случай", "m", "P отк", "Q", "_n", "kn", "t_c");
			for (int i = 0; i < 3; i++)
			{
				table.AddRow(dataRes[i,6], dataRes[i, 0], dataRes[i, 1], dataRes[i, 2], dataRes[i, 3], dataRes[i, 4], dataRes[i, 5]);
			}
			table.Write();
		}

		public static void M_M_n_0(double lam, double mu, int n, int m) 
		{
			//var S_0 = n + 1;
			var P_0 = 1.0;
			var P_k = 0.0;
			var a = lam / mu;
			var sum_P = 0.0;

			Console.WriteLine("Нахождение вероятности того, что в системе не находится заявка...");

			for (var i = 0; i < n; i++)
			{
				P_0 += (Math.Pow(a, i + 1) / Fact(i + 1));
			}
			P_0 = 1 / P_0;
			sum_P += P_0;
			P_otk = (Math.Pow(a, n) / Fact(n)) * P_0;
			Console.WriteLine($"Вероятность того, что в системе находится заявка: {P_0}");
			Console.Write("Вероятности того, что в системе находится k заявок: ");
			for (var k = 1; k < n + 1; k++)
			{
				P_k = (Math.Pow(a, k) / Fact(k)) * P_0;
				sum_P += P_k;
				Console.Write($" {P_k} ");
			}
			Console.WriteLine("\nПроверка суммы вероятностей...");
			Console.WriteLine($"Сумма вероятностей равна: {sum_P}");
			Console.WriteLine($"Вероятность отказа в обслуживании: {P_otk}");
			Q = lam * (1 - P_otk);
			Console.WriteLine($"Пропускная способность: {Q}");
			_n = a * (1 - P_otk);
			Console.WriteLine($"Среднее число занятых приборов: {_n}");
			t_s = _n / lam;
			Console.WriteLine($"Среднее время нахождения заявки в системе: {t_s}");
			k_n = (_n / n) * 100;
			Console.WriteLine($"Процент занятости приборов: {k_n}");
			dataRes[0, 0] = "0";
			dataRes[0, 1] = P_otk.ToString();
			dataRes[0, 2] = Q.ToString();
			dataRes[0, 3] = _n.ToString();
			dataRes[0, 4] = k_n.ToString();
			dataRes[0, 5] = t_s.ToString();
			dataRes[0, 6] = "M/M/n/0";
		}

		public static void M_M_n_inf(double lam, double mu, int n, int m) 
		{
			var a = lam / mu;
			var P_0 = 1.0;
			var sum_P = 0.0;

			Console.WriteLine("Нахождение вероятности того, что в системе не находится заявка...");

			for (var k = 0; k < n; k++)
			{
				P_0 += (Math.Pow(a, k + 1) / Fact(k + 1));
			}
			P_0 += Math.Pow(a, n + 1) / (Fact(n) * (n - a));
			P_0 = 1 / P_0;
			sum_P += P_0;
			Console.WriteLine($"Вероятность того, что в системе находится заявка: {P_0}");
			Console.Write("Вероятности того, что в системе находится k заявок: ");
			var sum_p_k = 0.0;
			for (var i = 0; i < n; i++)
			{
				var p_k = (Math.Pow(a, i+1) / Fact(i+1)) * P_0;
				sum_P += p_k;
				Console.Write($" {p_k} ");
			}
			for (var i = 0; i < n - 1; i++)
			{
				var p_k = (Math.Pow(a, i + 1) / Fact(i + 1)) * P_0;
				sum_p_k += p_k;
			}
			Console.WriteLine("\nПроверка суммы вероятностей...");
			Console.WriteLine($"Сумма вероятностей равна: {sum_P}");
			P_och = 1 - sum_p_k;
			Console.WriteLine($"Вероятность отказа в обслуживании: {P_och}");
			_n = a;
			Console.WriteLine($"Среднее число занятых приборов: {_n}");
			_m = (a * P_och) / (n - a);
			Console.WriteLine($"Среднее число заявок в очереди: {_m}");
			_N = _n + _m;
			Console.WriteLine($"Среднее число заявок в системе: {_N}");
			Q = lam;
			Console.WriteLine($"Пропускная способность: {Q}");
			t_och = _m / lam;
			Console.WriteLine($"Среднее время нахождения заявки в очереди: {t_och}");
			t_s = _N / lam;
			Console.WriteLine($"Среднее время нахождения заявки в системе: {t_s}");
			k_n = (_n / n) * 100;
			Console.WriteLine($"Процент занятости приборов: {k_n}");
			dataRes[1, 0] = "inf";
			dataRes[1, 1] = P_otk.ToString();
			dataRes[1, 2] = Q.ToString();
			dataRes[1, 3] = _n.ToString();
			dataRes[1, 4] = k_n.ToString();
			dataRes[1, 5] = t_s.ToString();
			dataRes[1, 6] = "M/M/n/inf";
		}

		public static void M_M_n_m(double lam, double mu, int n, int m)
		{
			var S_0 = n + m + 1;
			var P_0 = 1.0;
			var a = lam / mu;
			var P_k = 0.0;
			var P_n_s = 0.0;
			var sum_P = 0.0;

			Console.WriteLine("Нахождение вероятности того, что в системе не находится заявка...");

			for (var k = 1; k < n + 1 ; k++)
			{
				P_0 += Math.Pow(a, k) / Fact(k);
			}
			var s_a = 0.0;
			for (var s = 1; s < m + 1; s++)
			{
				s_a += Math.Pow(a / n, s);
			}
			P_0 += (Math.Pow(a, n) / Fact(n)) * s_a;
			P_0 = Math.Pow(P_0, -1);
			Console.WriteLine($"Вероятность того, что в системе находится заявка: {P_0}");
			sum_P += P_0;
			Console.Write("Вероятности того, что в системе находится k заявок: ");
			for (var k = 1; k < n + 1; k++)
			{
				P_k = (Math.Pow(a, k) / Fact(k)) * P_0;
				sum_P += P_k;
				Console.Write($" {P_k} ");
			}
			Console.Write("\nВероятности того, что в системе находится n+s заявок: ");
			for (var s = 1; s < m + 1; s++)
			{
				P_n_s = (Math.Pow(a, n) / Fact(n)) * Math.Pow(a / n, s) * P_0;
				sum_P += P_n_s;
				Console.Write($" {P_n_s} ");
			}
			Console.WriteLine("\nПроверка суммы вероятностей...");
			Console.WriteLine($"Сумма вероятностей равна: {sum_P}");

			P_otk = (Math.Pow(a, n) / Fact(n)) * Math.Pow(a / n, m) * P_0;
			Console.WriteLine($"Вероятность отказа в обслуживании: {P_otk}");
			Q = lam * (1 - P_otk);
			Console.WriteLine($"Пропускная способность: {Q}");
			_n = a * (1 - P_otk);
			Console.WriteLine($"Среднее число занятых приборов: {_n}");
			_m = 0.0;
			for (var s = 1; s < m + 1; s++)
			{
				_m += s * (Math.Pow(a, n) / Fact(n)) * Math.Pow(a / n, s) * P_0;
			}
			Console.WriteLine($"Среднее число заявок в очереди: {_m}");
			_N = _n + _m;
			Console.WriteLine($"Среднее число заявок в системе: {_N}");
			t_och = _m / lam;
			Console.WriteLine($"Среднее время нахождения заявки в очереди: {t_och}");
			t_s = _N / lam;
			Console.WriteLine($"Среднее время нахождения заявки в системе: {t_s}");
			k_n = (_n / n) * 100;
			Console.WriteLine($"Процент занятости приборов: {k_n}");
			dataRes[2, 0] = m.ToString();
			dataRes[2, 1] = P_otk.ToString();
			dataRes[2, 2] = Q.ToString();
			dataRes[2, 3] = _n.ToString();
			dataRes[2, 4] = k_n.ToString();
			dataRes[2, 5] = t_s.ToString();
			dataRes[2, 6] = "M/M/n/m";
		}

		public static int Fact(int x)
		{
			if (x == 1 || x == 0)
				return 1;
			return x * Fact(x - 1);
		}
	}
}
