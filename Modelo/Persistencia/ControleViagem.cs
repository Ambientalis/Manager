using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia.Fabrica;
using Persistencia.Filtros;
using Persistencia.Modelo;

namespace Modelo
{
    public partial class ControleViagem : ObjetoBase
    {
        /// <summary>
        /// Consulta um objeto armazenado com o id de parametro
        /// </summary>
        /// <param name="id">O Id do objeto armazenado</param>
        /// <returns>Um objeto persistente com o id de parametro ou nulo caso não exista</returns>
        public static ControleViagem ConsultarPorId(int id)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            ControleViagem classe = new ControleViagem();
            if (id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
            {
                classe.Id = id;
                return fabrica.GetDAOBase().ConsultarPorID<ControleViagem>(classe);
            }
        }

        /// <summary>
        /// Consulta o corrente objeto em banco, tornando-o persistente
        /// </summary>
        public virtual ControleViagem ConsultarPorId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            if (this.Id < 0)
                throw new ArgumentException("Id não pode ser negativo.");
            else
                return fabrica.GetDAOBase().ConsultarPorID<ControleViagem>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco
        /// </summary>
        public virtual ControleViagem Salvar()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Salvar<ControleViagem>(this);
        }

        /// <summary>
        /// Salva a corrente instância em banco forçando o id
        /// </summary>
        public virtual ControleViagem SalvarComId()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarComId<ControleViagem>(this);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<ControleViagem> SalvarTodos(IList<ControleViagem> objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<ControleViagem>(objs);
        }

        /// <summary>
        /// Salva uma lista de objetos, tornando-os persistentes
        /// </summary>
        /// <param name="objetos">Os objetos a serem salvos</param>
        public virtual IList<ControleViagem> SalvarTodos(params ControleViagem[] objs)
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().SalvarTodos<ControleViagem>(objs);
        }

        /// <summary>
        /// Destaca a corrente instancia do cache do Hibernate
        /// </summary>
        public virtual bool DestacarObjeto()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().DestacarObjeto<ControleViagem>(this);
        }

        /// <summary>
        /// Exclui a corrente instância do banco
        /// </summary>
        public virtual bool Excluir()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().Excluir<ControleViagem>(this);
        }

        /// <summary>
        /// Retorna todos os objetos armazenados em banco deste Tipo
        /// </summary>
        /// <returns>Uma lista de objetos armazenados</returns>
        public static IList<ControleViagem> ConsultarTodos()
        {
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            ControleViagem obj = Activator.CreateInstance<ControleViagem>();
            return fabrica.GetDAOBase().ConsultarTodos<ControleViagem>(obj);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem ascendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<ControleViagem> ConsultarOrdemAcendente(int qtd)
        {
            ControleViagem ee = new ControleViagem();
            ee.AdicionarFiltro(Filtros.SetOrderAsc("Data"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ControleViagem>(ee);
        }

        /// <summary>
        /// Método que retorna uma lista limitada de objetos em ordem descendente do Nome
        /// </summary>
        /// <param name="qtd">A quantidade de objetos a serem retornados na lista, o Valor 0 retorna todos</param>
        /// <returns>Uma lista limitada de objetos ordenados pelo Nome</returns>
        public virtual IList<ControleViagem> ConsultarOrdemDescendente(int qtd)
        {
            ControleViagem ee = new ControleViagem();
            ee.AdicionarFiltro(Filtros.SetOrderDesc("Data"));
            if (qtd > 0)
                ee.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ControleViagem>(ee);
        }

        /// <summary>
        /// Filtra uma certa quantidade de Cidade
        /// </summary>
        /// <param name="qtd">A quantidade de objetos que devem ser retornados no máximo,valores menores que 1 retornam todos os objetos</param>
        /// <returns>Uma lista com a quantidade de objetos</returns>
        public virtual IList<ControleViagem> Filtrar(int qtd)
        {
            ControleViagem estado = new ControleViagem();
            if (qtd > 0)
                estado.AdicionarFiltro(Filtros.MaxResults(qtd));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ControleViagem>(estado);
        }

        /// <summary>
        /// Retorna o ultimo Cidade Inserido no banco de dados
        /// </summary>
        /// <returns>O ultimo Cidade</returns>
        public virtual ControleViagem UltimoInserido()
        {
            ControleViagem estado = new ControleViagem();
            estado.AdicionarFiltro(Filtros.Max("Id"));
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarUnicoComFiltro<ControleViagem>(estado);
        }

        /// <summary>
        /// Consulta todos os Cidades armazenados, ordenando-os pelo Nome
        /// </summary>
        /// <returns>A lista de todos os Cidades armazenados ordenados pelo Nome</returns>
        public static IList<ControleViagem> ConsultarTodosOrdemAlfabetica()
        {
            ControleViagem aux = new ControleViagem();
            aux.AdicionarFiltro(Filtros.SetOrderAsc("Data"));
            aux.AdicionarFiltro(Filtros.Distinct());
            FabricaDAONHibernateBase fabrica = new FabricaDAONHibernateBase();
            return fabrica.GetDAOBase().ConsultarComFiltro<ControleViagem>(aux);
        }

        public static IList<ControleViagem> Filtrar(Permissao permissao,
            DateTime dataDe, DateTime dataAte, int idResponsavel, int idMotorista, string roteiro,
            int idDepartamentoVeiculo, int idVeiculo, int idDepartamentoUtilizou, int idSetorUtilizou, int possuiAbastecimento)
        {
            if (permissao == null || permissao.VisualizaControleViagens.Equals(Permissao.NENHUMA))
                return null;

            ControleViagem aux = new ControleViagem();
            switch (permissao.VisualizaControleViagens)
            {
                case Permissao.RESPONSAVEL:
                    aux.AdicionarFiltro(Filtros.Eq("Responsavel.Id", permissao.Funcionario.Id));
                    break;
                case Permissao.SETOR:
                    aux.AdicionarFiltro(Filtros.CriarAlias("SetorQueUtilizou", "setor"));
                    aux.AdicionarFiltro(Filtros.Eq("setor.Id", permissao.Funcao.Setor.Id));
                    break;
                case Permissao.DEPARTAMENTO:
                    aux.AdicionarFiltro(Filtros.CriarAlias("SetorQueUtilizou", "setor"));
                    aux.AdicionarFiltro(Filtros.CriarAlias("setor.Departamento", "dept"));
                    aux.AdicionarFiltro(Filtros.Eq("dept.Id", permissao.Funcao.Setor.Departamento.Id));
                    break;
            }

            aux.AdicionarFiltro(Filtros.Between("Data", dataDe, dataAte));
            if (idResponsavel > 0)
                aux.AdicionarFiltro(Filtros.Eq("Responsavel.Id", idResponsavel));
            if (idMotorista > 0)
                aux.AdicionarFiltro(Filtros.Eq("Motorista.Id", idMotorista));
            if (!string.IsNullOrEmpty(roteiro))
                aux.AdicionarFiltro(Filtros.Eq("Roteiro", roteiro));
            if (idDepartamentoVeiculo > 0 || idVeiculo > 0)
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("Veiculo", "vei"));
                if (idVeiculo > 0)
                    aux.AdicionarFiltro(Filtros.Eq("vei.Id", idVeiculo));
                else if (idDepartamentoVeiculo > 0)
                {
                    aux.AdicionarFiltro(Filtros.CriarAlias("vei.DepartamentoResponsavel", "deptVeiculo"));
                    aux.AdicionarFiltro(Filtros.Eq("deptVeiculo.Id", idDepartamentoVeiculo));
                }
            }

            if (idDepartamentoUtilizou > 0 || idSetorUtilizou > 0)
            {
                aux.AdicionarFiltro(Filtros.CriarAlias("SetorQueUtilizou", "setor2"));
                if (idSetorUtilizou > 0)
                    aux.AdicionarFiltro(Filtros.Eq("setor2.Id", idSetorUtilizou));
                else if (idDepartamentoUtilizou > 0)
                {
                    aux.AdicionarFiltro(Filtros.CriarAlias("setor2.Departamento", "deptUtilizou"));
                    aux.AdicionarFiltro(Filtros.Eq("deptUtilizou.Id", idDepartamentoUtilizou));
                }
            }

            if (possuiAbastecimento > 0)
                if (possuiAbastecimento == 1)
                {
                    aux.AdicionarFiltro(Filtros.CriarAlias("Abastecimentos", "abs"));
                    aux.AdicionarFiltro(Filtros.Maior("abs.Id", 0));
                }
            IList<ControleViagem> retorno = new FabricaDAONHibernateBase().GetDAOBase().ConsultarComFiltro<ControleViagem>(aux);
            if (possuiAbastecimento == 2)
                retorno = retorno.Where(controle => controle.Abastecimentos == null || controle.Abastecimentos.Count < 1).ToList();
            return retorno;
        }

        public virtual String GetDescricaoResponsavel
        {
            get
            {
                return this.Responsavel != null ? this.Responsavel.NomeRazaoSocial : "";
            }
        }

        public virtual String GetDescricaoMotorista
        {
            get
            {
                return this.Motorista != null ? this.Motorista.NomeRazaoSocial : "";
            }
        }

        public virtual String GetDescricaoVeiculo
        {
            get
            {
                return this.Veiculo != null ? this.Veiculo.Descricao : "";
            }
        }

        public virtual String GetDescricaoSetorUtilizou
        {
            get
            {
                return this.SetorQueUtilizou != null ? this.SetorQueUtilizou.Nome : "";
            }
        }

        public virtual String GetDescricaoHoraChegada
        {
            get
            {
                if (this.DataHoraChegada <= DateTime.MinValue || this.DataHoraChegada >= DateTime.MaxValue)
                    return "";
                else
                    return this.DataHoraChegada.ToString("dd/MM/yyyy HH:mm:ss");
            }
        }

        public virtual String GetDescricaoQuilometragemChegada
        {
            get
            {
                return this.QuilometragemChegada > 0 ? this.QuilometragemChegada.ToString("#0.0") : "";
            }
        }

        public virtual String GetDescricaoAbastecimentos
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                if (this.Abastecimentos != null)
                    foreach (Abastecimento aba in this.Abastecimentos)
                        builder.Append(aba.Data).Append(" - ")
                            .Append(String.Format("{0:C}", Convert.ToDecimal(aba.ValorTotal)))
                            .Append("<br />");
                return builder.ToString();
            }
        }

        public virtual long GetTempoPermanenciaVeiculoTicks
        {
            get
            {
                DateTime dataChegada = this.DataHoraChegada <= SqlDate.MinValue ? DateTime.Now : this.DataHoraChegada;
                return dataChegada.Subtract(this.DataHoraSaida).Ticks;
            }
        }

        public virtual TimeSpan GetTempoPermanenciaVeiculo
        {
            get
            {
                return new TimeSpan(this.GetTempoPermanenciaVeiculoTicks);
            }
        }

        public virtual decimal GetKmRodados
        {
            get
            {
                return this.QuilometragemChegada > 0 ? (this.QuilometragemChegada - this.QuilometragemSaida) : 0;
            }
        }

        public virtual decimal GetTotalLitrosAbastecidos
        {
            get
            {
                decimal total = 0;
                if (this.Abastecimentos != null)
                    total = this.Abastecimentos.Sum(aba => aba.QtdLitros);
                return total;
            }
        }

        public virtual decimal GetValorMedioLitroAbastecido
        {
            get
            {
                return this.Abastecimentos != null && this.Abastecimentos.Count > 0 ? this.Abastecimentos.Average(control => control.ValorUnitario) : 0;
            }
        }

        public virtual String GetGastoMedio
        {
            get
            {
                decimal totalAbastecimento = this.GetGastoTotal;
                decimal totalKmRodados = this.GetKmRodados;
                totalKmRodados = totalKmRodados > 0 ? totalKmRodados : 1;
                decimal totalLitrosConsumidos = this.GetTotalLitrosAbastecidos;
                totalLitrosConsumidos = totalLitrosConsumidos > 0 ? totalLitrosConsumidos : 1;

                return "R$ " + (totalAbastecimento / totalLitrosConsumidos).ToString("#0.00") + "/litro ; <br /> "
                    + "R$ " + (totalAbastecimento / totalKmRodados).ToString("#0.00") + "/Km";
            }
        }

        public virtual decimal GetGastoTotal
        {
            get
            {
                decimal total = 0;
                if (this.Abastecimentos != null)
                    total = this.Abastecimentos.Sum(aba => aba.ValorTotal);
                return total;
            }
        }
    }
}
