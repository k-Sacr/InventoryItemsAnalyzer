using System;
using System.Collections.Generic;
using ExileCore;
using ExileCore.PoEMemory.FilesInMemory;
using ExileCore.Shared.Enums;

namespace InventoryItemsAnalyzer
{
    public class ModRecordCache
    {
        private readonly GameController _gameController;
        public bool InitializedModRecords { get; private set; }
        public IDictionary<string, ModsDat.ModRecord> ModRecords { get; private set; }
        public bool InitializedModRecordsByTier { get; private set; }
        public IDictionary<Tuple<string, ModType>, List<ModsDat.ModRecord>> ModRecordsByTier { get; private set; }

        public ModRecordCache(
            GameController gameController)
        {
            _gameController = gameController;
            InitializedModRecords = false;
        }

        public void Initialize()
        {
            if (!InitializedModRecords)
            {
                try
                {
                    var records = _gameController?.Files?.Mods?.records;
                    if (records?.Count > 1)
                    {
                        ModRecords = new Dictionary<string, ModsDat.ModRecord>();
                        foreach (var pair in records)
                        {
                            ModRecords.Add(pair.Key, pair.Value);
                        }
                    }
                }
                catch (Exception)
                {
                    InitializedModRecords = false;
                }
                finally
                {
                    InitializedModRecords = true;
                }                
            }
            
            if (!InitializedModRecordsByTier)
            {
                try
                {
                    var records = _gameController?.Files?.Mods?.recordsByTier;
                    if (records?.Count > 1)
                    {
                        ModRecordsByTier = new Dictionary<Tuple<string, ModType>, List<ModsDat.ModRecord>>();
                        foreach (var pair in records)
                        {
                            ModRecordsByTier.Add(pair.Key, pair.Value);
                        }
                    }
                }
                catch (Exception)
                {
                    InitializedModRecordsByTier = false;
                }
                finally
                {
                    InitializedModRecordsByTier = true;
                }                
            }
        }
    }
}