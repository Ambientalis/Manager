using System;
using System.Collections.Generic;
using Persistencia.Filtros;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;
using NHibernate.Mapping.Attributes;

namespace Persistencia.Modelo
{
    /// <summary>
    /// Classe base para todos os objetos persistentes. 
    /// Contém o id dos objetos e uma lista de filtros a serem utilizadas em consultas
    /// </summary>
    [Serializable]
    public class ObjetoBase : ICloneable
    {
        #region ______________ Construtor ______________

        public ObjetoBase() { }

        #endregion

        #region ______________ Atributos _______________

        private int _id;
        private bool ativo = true;
        private int empresa;
        private int version;
        private bool multiEmpresa = true;
        private IList<IFiltro> listaFiltros = new List<IFiltro>();

        #endregion

        #region ______________ Propriedades ____________

        [Id(0, Column = "id", Type = "Int32", UnsavedValue = "0", Name = "Id")]
        [Generator(1, Class = "native")]
        public virtual int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        [Property(Type = "TrueFalse")]
        public virtual bool Ativo
        {
            get { return ativo; }
            set { ativo = value; }
        }

        public virtual bool MultiEmpresa
        {
            get { return multiEmpresa; }
            set { multiEmpresa = value; }
        }

        [Version(2, Column = "version", Type = "Int32", Generated = VersionGeneration.Never, UnsavedValue = "0")]
        public virtual int Version
        {
            get { return version; }
            set { version = value; }
        }

        [Property(Type = "Int32")]
        public virtual int Emp
        {
            get { return empresa; }
            set { empresa = value; }
        }

        /// <summary>
        /// Lista de Filtros utilizada em uma consulta do framework
        /// <para>
        /// Veja também as classes de C2Framework.Persistencia.Filtro
        /// 
        /// </para>
        /// </summary>
        public virtual IList<IFiltro> ListaFiltros
        {
            get { return listaFiltros; }
            set { listaFiltros = value; }
        }

        #endregion

        #region ______________ Métodos _________________

        /// <summary>
        /// Sobrescrita do método Equals para comparar por ID
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            try
            {
                if (this.Id == 0 && ((ObjetoBase)obj).Id == 0)
                    return this == obj;
                else
                    return this.Id == ((ObjetoBase)obj).Id;
            }
            catch (Exception) { return base.Equals(obj); }
        }

        public virtual void AdicionarFiltro(IFiltro f)
        {
            ListaFiltros.Add(f);
        }

        #region _________ Clone ___________

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

        public virtual T CloneObject<T>()
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter(null,
                     new StreamingContext(StreamingContextStates.Clone));
                binaryFormatter.Serialize(memStream, this);
                memStream.Seek(0, SeekOrigin.Begin);
                return (T)binaryFormatter.Deserialize(memStream);
            }
        }

        #endregion

        #endregion

        #region ______________ Métodos Básicos de Persistência _________________

        #endregion

        public virtual object GetValorMetodo(string nomeMetodo)
        {
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(nomeMetodo);
            if (methodInfo != null)
                return methodInfo.Invoke(this, null);
            return null;
        }
    }
}
