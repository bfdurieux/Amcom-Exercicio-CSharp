using System;
using System.Globalization;

namespace Questao1
{
    class ContaBancaria
    {
        private const double TransactionTax = 3.50;
        public int NumeroConta { get; }
        public string NomeTitular { get; set; }
        public double Saldo { get; private set; }

        private CultureInfo _culture = CultureInfo.CreateSpecificCulture("en-US");

        public ContaBancaria(int numeroConta, string nomeTitular, double depositoInicial = 0)
        {
            NumeroConta = numeroConta;
            NomeTitular = nomeTitular;
            Saldo = depositoInicial;
        }

        public void Deposito(double value)
        {
            Saldo += value;
        }

        public void Saque(double value)
        {
            Saldo -= value + TransactionTax;
        }
        
        public override string ToString()
        {
            _culture.NumberFormat.CurrencyNegativePattern = 1;
            var formattedSaldo = Saldo.ToString("C2", _culture);
            
            return $"Conta: {NumeroConta}, Titular: {NomeTitular}, Saldo: {formattedSaldo}";
        }
    }
}
