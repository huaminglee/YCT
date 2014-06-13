using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.IO;
using System.Collections.ObjectModel;

namespace EDoc2.YCT.Core
{
    public class ContractFactory
    {
        List<Contract> _contracts;

        Dictionary<int, Contract> _contractsDictionaryForFileId;

        private object _contractListLock = new object();

        public ContractFactory()
        {
            _contracts = new List<Contract>();
            _contractsDictionaryForFileId = new Dictionary<int, Contract>();
            this.Load();
        }

        public Contract Create(int edoc2FileId, DateTime? expireDate, int? validMonth)
        {
            lock (_contractListLock)
            {
                ContractInfo contractInfo = new ContractInfo();
                contractInfo.EDoc2FileId = edoc2FileId;
                contractInfo.ExpireDate = expireDate;
                contractInfo.ValidMonth = validMonth;
                object id = NHibernateHelper.CurrentSession.Save(contractInfo);
                contractInfo.Id = int.Parse(id.ToString());
                Contract contract = new Contract(contractInfo);

                List<Contract> contracts = new List<Contract>();
                contracts.AddRange(this._contracts);
                contracts.Add(contract);
                this._contracts = contracts;
                this.Index();
                return contract;
            }
        }

        public ReadOnlyCollection<Contract> Contracts
        {
            get
            {
                return this._contracts.AsReadOnly();
            }
        }

        public bool Exists(int edoc2FileId)
        {
            return this._contractsDictionaryForFileId.ContainsKey(edoc2FileId);
        }

        public Contract GetContractByFileId(int edoc2FileId)
        {
            if (this._contractsDictionaryForFileId.ContainsKey(edoc2FileId))
            {
                return this._contractsDictionaryForFileId[edoc2FileId];
            }
            return null;
        }

        private bool _loaded;

        private void Load()
        {
            if (!_loaded)
            {
                lock (_contractListLock)
                {
                    if (!_loaded)
                    {
                        List<ContractInfo> contractInfos = NHibernateHelper.CurrentSession.QueryOver<ContractInfo>().List().ToList();
                        if (contractInfos != null)
                        {
                            foreach (ContractInfo contractInfo in contractInfos)
                            {
                                Contract contract = new Contract(contractInfo);
                                this._contracts.Add(contract);
                            }
                            this.Index();
                        }
                        _loaded = true;
                    }
                }
            }
        }

        private void Index()
        {
            _contractsDictionaryForFileId = this._contracts.ToDictionary(x => x.EDoc2FileId);
        }
    }
}
