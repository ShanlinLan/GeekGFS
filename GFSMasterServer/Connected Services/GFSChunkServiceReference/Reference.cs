﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace GFSMasterServer.GFSChunkServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GFSChunkServiceReference.IGFSChunkServer")]
    public interface IGFSChunkServer {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSChunkServer/Write", ReplyAction="http://tempuri.org/IGFSChunkServer/WriteResponse")]
        bool Write(string chunkServerId, string chunkUuid, byte[] chunkData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSChunkServer/Write", ReplyAction="http://tempuri.org/IGFSChunkServer/WriteResponse")]
        System.Threading.Tasks.Task<bool> WriteAsync(string chunkServerId, string chunkUuid, byte[] chunkData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSChunkServer/Read", ReplyAction="http://tempuri.org/IGFSChunkServer/ReadResponse")]
        byte[] Read(string chunkServerId, string chunkUuid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSChunkServer/Read", ReplyAction="http://tempuri.org/IGFSChunkServer/ReadResponse")]
        System.Threading.Tasks.Task<byte[]> ReadAsync(string chunkServerId, string chunkUuid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSChunkServer/Delete", ReplyAction="http://tempuri.org/IGFSChunkServer/DeleteResponse")]
        bool Delete(string chunkServerId, string chunkUuid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGFSChunkServer/Delete", ReplyAction="http://tempuri.org/IGFSChunkServer/DeleteResponse")]
        System.Threading.Tasks.Task<bool> DeleteAsync(string chunkServerId, string chunkUuid);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGFSChunkServerChannel : GFSMasterServer.GFSChunkServiceReference.IGFSChunkServer, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GFSChunkServerClient : System.ServiceModel.ClientBase<GFSMasterServer.GFSChunkServiceReference.IGFSChunkServer>, GFSMasterServer.GFSChunkServiceReference.IGFSChunkServer {
        
        public GFSChunkServerClient() {
        }
        
        public GFSChunkServerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public GFSChunkServerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GFSChunkServerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GFSChunkServerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Write(string chunkServerId, string chunkUuid, byte[] chunkData) {
            return base.Channel.Write(chunkServerId, chunkUuid, chunkData);
        }
        
        public System.Threading.Tasks.Task<bool> WriteAsync(string chunkServerId, string chunkUuid, byte[] chunkData) {
            return base.Channel.WriteAsync(chunkServerId, chunkUuid, chunkData);
        }
        
        public byte[] Read(string chunkServerId, string chunkUuid) {
            return base.Channel.Read(chunkServerId, chunkUuid);
        }
        
        public System.Threading.Tasks.Task<byte[]> ReadAsync(string chunkServerId, string chunkUuid) {
            return base.Channel.ReadAsync(chunkServerId, chunkUuid);
        }
        
        public bool Delete(string chunkServerId, string chunkUuid) {
            return base.Channel.Delete(chunkServerId, chunkUuid);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteAsync(string chunkServerId, string chunkUuid) {
            return base.Channel.DeleteAsync(chunkServerId, chunkUuid);
        }
    }
}
