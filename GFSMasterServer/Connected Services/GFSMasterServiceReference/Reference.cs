﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace GFSMasterServer.GFSMasterServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GFSMasterServiceReference.IGFSMaster")]
    public interface IGFSMaster {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/GetDeletedChunkServerId", ReplyAction="http://tempuri.org/IGFSMaster/GetDeletedChunkServerIdResponse")]
        string GetDeletedChunkServerId(string chunkUuid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/GetDeletedChunkServerId", ReplyAction="http://tempuri.org/IGFSMaster/GetDeletedChunkServerIdResponse")]
        System.Threading.Tasks.Task<string> GetDeletedChunkServerIdAsync(string chunkUuid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/Get", ReplyAction="http://tempuri.org/IGFSMaster/GetResponse")]
        System.Collections.Generic.KeyValuePair<int, int> Get();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/Get", ReplyAction="http://tempuri.org/IGFSMaster/GetResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.KeyValuePair<int, int>> GetAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/GetLogs", ReplyAction="http://tempuri.org/IGFSMaster/GetLogsResponse")]
        System.Collections.Generic.List<string> GetLogs();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/GetLogs", ReplyAction="http://tempuri.org/IGFSMaster/GetLogsResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<string>> GetLogsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/GetChunkTable", ReplyAction="http://tempuri.org/IGFSMaster/GetChunkTableResponse")]
        System.Collections.Generic.Dictionary<string, string> GetChunkTable();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/GetChunkTable", ReplyAction="http://tempuri.org/IGFSMaster/GetChunkTableResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> GetChunkTableAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/Write_GetChunkUuidAndChunkServerId", ReplyAction="http://tempuri.org/IGFSMaster/Write_GetChunkUuidAndChunkServerIdResponse")]
        System.Collections.Generic.KeyValuePair<string, string> Write_GetChunkUuidAndChunkServerId(string fileName, int chunkIndex);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/Write_GetChunkUuidAndChunkServerId", ReplyAction="http://tempuri.org/IGFSMaster/Write_GetChunkUuidAndChunkServerIdResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.KeyValuePair<string, string>> Write_GetChunkUuidAndChunkServerIdAsync(string fileName, int chunkIndex);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/WriteFileInfo", ReplyAction="http://tempuri.org/IGFSMaster/WriteFileInfoResponse")]
        void WriteFileInfo(string fileName, System.Collections.Generic.List<string> _fileInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/WriteFileInfo", ReplyAction="http://tempuri.org/IGFSMaster/WriteFileInfoResponse")]
        System.Threading.Tasks.Task WriteFileInfoAsync(string fileName, System.Collections.Generic.List<string> _fileInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/Read_GetChunkUuidAndChunkServerId", ReplyAction="http://tempuri.org/IGFSMaster/Read_GetChunkUuidAndChunkServerIdResponse")]
        System.Collections.Generic.KeyValuePair<string, string> Read_GetChunkUuidAndChunkServerId(string fileName, int chunkIndex);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/Read_GetChunkUuidAndChunkServerId", ReplyAction="http://tempuri.org/IGFSMaster/Read_GetChunkUuidAndChunkServerIdResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.KeyValuePair<string, string>> Read_GetChunkUuidAndChunkServerIdAsync(string fileName, int chunkIndex);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/GetNumOfChunks", ReplyAction="http://tempuri.org/IGFSMaster/GetNumOfChunksResponse")]
        string GetNumOfChunks(string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/GetNumOfChunks", ReplyAction="http://tempuri.org/IGFSMaster/GetNumOfChunksResponse")]
        System.Threading.Tasks.Task<string> GetNumOfChunksAsync(string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/GetAllFileInfo", ReplyAction="http://tempuri.org/IGFSMaster/GetAllFileInfoResponse")]
        System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> GetAllFileInfo();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/GetAllFileInfo", ReplyAction="http://tempuri.org/IGFSMaster/GetAllFileInfoResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>> GetAllFileInfoAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/AddChunkServer", ReplyAction="http://tempuri.org/IGFSMaster/AddChunkServerResponse")]
        void AddChunkServer(string chunkServerId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/AddChunkServer", ReplyAction="http://tempuri.org/IGFSMaster/AddChunkServerResponse")]
        System.Threading.Tasks.Task AddChunkServerAsync(string chunkServerId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/Exists", ReplyAction="http://tempuri.org/IGFSMaster/ExistsResponse")]
        bool Exists(string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/Exists", ReplyAction="http://tempuri.org/IGFSMaster/ExistsResponse")]
        System.Threading.Tasks.Task<bool> ExistsAsync(string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/Delete", ReplyAction="http://tempuri.org/IGFSMaster/DeleteResponse")]
        void Delete(string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/Delete", ReplyAction="http://tempuri.org/IGFSMaster/DeleteResponse")]
        System.Threading.Tasks.Task DeleteAsync(string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/GetFileTable", ReplyAction="http://tempuri.org/IGFSMaster/GetFileTableResponse")]
        System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> GetFileTable();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/GetFileTable", ReplyAction="http://tempuri.org/IGFSMaster/GetFileTableResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>> GetFileTableAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/GetDeletedFile", ReplyAction="http://tempuri.org/IGFSMaster/GetDeletedFileResponse")]
        System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> GetDeletedFile();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/GetDeletedFile", ReplyAction="http://tempuri.org/IGFSMaster/GetDeletedFileResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>> GetDeletedFileAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/GetChunkServerId", ReplyAction="http://tempuri.org/IGFSMaster/GetChunkServerIdResponse")]
        string GetChunkServerId(string chunkUuid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/GetChunkServerId", ReplyAction="http://tempuri.org/IGFSMaster/GetChunkServerIdResponse")]
        System.Threading.Tasks.Task<string> GetChunkServerIdAsync(string chunkUuid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/GetLengthOfFile", ReplyAction="http://tempuri.org/IGFSMaster/GetLengthOfFileResponse")]
        string GetLengthOfFile(string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/GetLengthOfFile", ReplyAction="http://tempuri.org/IGFSMaster/GetLengthOfFileResponse")]
        System.Threading.Tasks.Task<string> GetLengthOfFileAsync(string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/AddBackUp", ReplyAction="http://tempuri.org/IGFSMaster/AddBackUpResponse")]
        void AddBackUp(System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> _fileTable, System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> _fileInfo, System.Collections.Generic.Dictionary<string, string> _chunkTable);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSMaster/AddBackUp", ReplyAction="http://tempuri.org/IGFSMaster/AddBackUpResponse")]
        System.Threading.Tasks.Task AddBackUpAsync(System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> _fileTable, System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> _fileInfo, System.Collections.Generic.Dictionary<string, string> _chunkTable);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGFSMasterChannel : GFSMasterServer.GFSMasterServiceReference.IGFSMaster, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GFSMasterClient : System.ServiceModel.ClientBase<GFSMasterServer.GFSMasterServiceReference.IGFSMaster>, GFSMasterServer.GFSMasterServiceReference.IGFSMaster {
        
        public GFSMasterClient() {
        }
        
        public GFSMasterClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public GFSMasterClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GFSMasterClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GFSMasterClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetDeletedChunkServerId(string chunkUuid) {
            return base.Channel.GetDeletedChunkServerId(chunkUuid);
        }
        
        public System.Threading.Tasks.Task<string> GetDeletedChunkServerIdAsync(string chunkUuid) {
            return base.Channel.GetDeletedChunkServerIdAsync(chunkUuid);
        }
        
        public System.Collections.Generic.KeyValuePair<int, int> Get() {
            return base.Channel.Get();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.KeyValuePair<int, int>> GetAsync() {
            return base.Channel.GetAsync();
        }
        
        public System.Collections.Generic.List<string> GetLogs() {
            return base.Channel.GetLogs();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<string>> GetLogsAsync() {
            return base.Channel.GetLogsAsync();
        }
        
        public System.Collections.Generic.Dictionary<string, string> GetChunkTable() {
            return base.Channel.GetChunkTable();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> GetChunkTableAsync() {
            return base.Channel.GetChunkTableAsync();
        }
        
        public System.Collections.Generic.KeyValuePair<string, string> Write_GetChunkUuidAndChunkServerId(string fileName, int chunkIndex) {
            return base.Channel.Write_GetChunkUuidAndChunkServerId(fileName, chunkIndex);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.KeyValuePair<string, string>> Write_GetChunkUuidAndChunkServerIdAsync(string fileName, int chunkIndex) {
            return base.Channel.Write_GetChunkUuidAndChunkServerIdAsync(fileName, chunkIndex);
        }
        
        public void WriteFileInfo(string fileName, System.Collections.Generic.List<string> _fileInfo) {
            base.Channel.WriteFileInfo(fileName, _fileInfo);
        }
        
        public System.Threading.Tasks.Task WriteFileInfoAsync(string fileName, System.Collections.Generic.List<string> _fileInfo) {
            return base.Channel.WriteFileInfoAsync(fileName, _fileInfo);
        }
        
        public System.Collections.Generic.KeyValuePair<string, string> Read_GetChunkUuidAndChunkServerId(string fileName, int chunkIndex) {
            return base.Channel.Read_GetChunkUuidAndChunkServerId(fileName, chunkIndex);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.KeyValuePair<string, string>> Read_GetChunkUuidAndChunkServerIdAsync(string fileName, int chunkIndex) {
            return base.Channel.Read_GetChunkUuidAndChunkServerIdAsync(fileName, chunkIndex);
        }
        
        public string GetNumOfChunks(string fileName) {
            return base.Channel.GetNumOfChunks(fileName);
        }
        
        public System.Threading.Tasks.Task<string> GetNumOfChunksAsync(string fileName) {
            return base.Channel.GetNumOfChunksAsync(fileName);
        }
        
        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> GetAllFileInfo() {
            return base.Channel.GetAllFileInfo();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>> GetAllFileInfoAsync() {
            return base.Channel.GetAllFileInfoAsync();
        }
        
        public void AddChunkServer(string chunkServerId) {
            base.Channel.AddChunkServer(chunkServerId);
        }
        
        public System.Threading.Tasks.Task AddChunkServerAsync(string chunkServerId) {
            return base.Channel.AddChunkServerAsync(chunkServerId);
        }
        
        public bool Exists(string fileName) {
            return base.Channel.Exists(fileName);
        }
        
        public System.Threading.Tasks.Task<bool> ExistsAsync(string fileName) {
            return base.Channel.ExistsAsync(fileName);
        }
        
        public void Delete(string fileName) {
            base.Channel.Delete(fileName);
        }
        
        public System.Threading.Tasks.Task DeleteAsync(string fileName) {
            return base.Channel.DeleteAsync(fileName);
        }
        
        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> GetFileTable() {
            return base.Channel.GetFileTable();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>> GetFileTableAsync() {
            return base.Channel.GetFileTableAsync();
        }
        
        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> GetDeletedFile() {
            return base.Channel.GetDeletedFile();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>> GetDeletedFileAsync() {
            return base.Channel.GetDeletedFileAsync();
        }
        
        public string GetChunkServerId(string chunkUuid) {
            return base.Channel.GetChunkServerId(chunkUuid);
        }
        
        public System.Threading.Tasks.Task<string> GetChunkServerIdAsync(string chunkUuid) {
            return base.Channel.GetChunkServerIdAsync(chunkUuid);
        }
        
        public string GetLengthOfFile(string fileName) {
            return base.Channel.GetLengthOfFile(fileName);
        }
        
        public System.Threading.Tasks.Task<string> GetLengthOfFileAsync(string fileName) {
            return base.Channel.GetLengthOfFileAsync(fileName);
        }
        
        public void AddBackUp(System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> _fileTable, System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> _fileInfo, System.Collections.Generic.Dictionary<string, string> _chunkTable) {
            base.Channel.AddBackUp(_fileTable, _fileInfo, _chunkTable);
        }
        
        public System.Threading.Tasks.Task AddBackUpAsync(System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> _fileTable, System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> _fileInfo, System.Collections.Generic.Dictionary<string, string> _chunkTable) {
            return base.Channel.AddBackUpAsync(_fileTable, _fileInfo, _chunkTable);
        }
    }
}
