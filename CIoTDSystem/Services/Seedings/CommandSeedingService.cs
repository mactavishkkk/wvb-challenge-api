using CIoTDSystem.Data;

namespace CIoTDSystem.Services.Seedings
{
    public class CommandSeedingService
    {
        private DataContext _context;

        public CommandSeedingService(DataContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Commands.Any())
            {
                return; // Database has been seeded
            }

            Command commandKDLAOIJ = new Command
            {
                DeviceId = 1,
                Name = "ObterTemperaturaAtual",
                Description = "Recupera a temperatura ambiente atual medida pelo sensor.",
                Parameters = "",
                ReturnType = "float"
            };
            Command commandOneKDLAOIJ = new Command
            {
                DeviceId = 1,
                Name = "DefinirLimiteSuperior",
                Description = "Define o limite superior de temperatura para acionar um alerta.",
                Parameters = "limiteSuperior",
                ReturnType = "void"
            };
            Command commandThreeKDLAOIJ = new Command
            {
                DeviceId = 1,
                Name = "DefinirLimiteInferior",
                Description = "Define o limite inferior de temperatura para acionar um alerta.",
                Parameters = "limiteInferior",
                ReturnType = "void"
            };

            Command commandLDOAIJ = new Command
            {
                DeviceId = 2,
                Name = "ObterUmidadeAtual",
                Description = "Recupera a porcentagem de umidade do solo atual medida pelo",
                Parameters = "",
                ReturnType = "float"
            };
            Command commandOneLDOAIJ = new Command
            {
                DeviceId = 2,
                Name = "ObterHistoricoUmidade",
                Description = "Recupera o histórico de medições de umidade do solo em um intervalo de tempo especificado.",
                Parameters = "dataInicio,dataFim",
                ReturnType = "List<>"
            };
            Command commandThreeLDOAIJ = new Command
            {
                DeviceId = 2,
                Name = "CalibrarSensor",
                Description = "Realiza a calibração do sensor de umidade do solo para garantir a precisão das medições.",
                Parameters = "valorReferencia",
                ReturnType = "void"
            };

            Command commandASDFGHJ = new Command
            {
                DeviceId = 3,
                Name = "IniciarIrrigacao",
                Description = "Aciona a irrigação em um ou mais setores definidos.",
                Parameters = "setores,duracaoIrrigacao,fluxoIrrigacao",
                ReturnType = "void"
            };
            Command commandOneASDFGHJ = new Command
            {
                DeviceId = 3,
                Name = "PausarIrrigacao",
                Description = "Interrompe a irrigação em andamento.",
                Parameters = "",
                ReturnType = "void"
            };
            Command commandThreeASDFGHJ = new Command
            {
                DeviceId = 3,
                Name = "ObterStatusIrrigacao",
                Description = "Retorna o status atual da irrigação (em andamento, pausada ou finalizada).",
                Parameters = "",
                ReturnType = "string"
            };

            _context.Commands.AddRange(
                commandKDLAOIJ, commandOneKDLAOIJ, commandThreeKDLAOIJ,
                commandLDOAIJ, commandOneLDOAIJ, commandThreeLDOAIJ,
                commandASDFGHJ, commandOneASDFGHJ, commandThreeASDFGHJ
            );
            _context.SaveChanges();
        }
    }
}
