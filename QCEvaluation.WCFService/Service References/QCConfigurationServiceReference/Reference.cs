﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QCEvaluation.WCFService.QCConfigurationServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetQualityControlRequest", Namespace="http://schemas.datacontract.org/2004/07/QCConfiguration.WCFService")]
    [System.SerializableAttribute()]
    public partial class GetQualityControlRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TestCodeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TestCode {
            get {
                return this.TestCodeField;
            }
            set {
                if ((this.TestCodeField.Equals(value) != true)) {
                    this.TestCodeField = value;
                    this.RaisePropertyChanged("TestCode");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetQualityControlResponse", Namespace="http://schemas.datacontract.org/2004/07/QCConfiguration.WCFService")]
    [System.SerializableAttribute()]
    public partial class GetQualityControlResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private QCEvaluation.WCFService.QCConfigurationServiceReference.QualityControlDTO QualityControlField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WCFServices.Common.RequestResult RequestResultField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public QCEvaluation.WCFService.QCConfigurationServiceReference.QualityControlDTO QualityControl {
            get {
                return this.QualityControlField;
            }
            set {
                if ((object.ReferenceEquals(this.QualityControlField, value) != true)) {
                    this.QualityControlField = value;
                    this.RaisePropertyChanged("QualityControl");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WCFServices.Common.RequestResult RequestResult {
            get {
                return this.RequestResultField;
            }
            set {
                if ((this.RequestResultField.Equals(value) != true)) {
                    this.RequestResultField = value;
                    this.RaisePropertyChanged("RequestResult");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="QualityControlDTO", Namespace="http://schemas.datacontract.org/2004/07/QCConfiguration.WCFService")]
    [System.SerializableAttribute()]
    public partial class QualityControlDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double StandardDeviationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double TargetValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TestCodeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double StandardDeviation {
            get {
                return this.StandardDeviationField;
            }
            set {
                if ((this.StandardDeviationField.Equals(value) != true)) {
                    this.StandardDeviationField = value;
                    this.RaisePropertyChanged("StandardDeviation");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double TargetValue {
            get {
                return this.TargetValueField;
            }
            set {
                if ((this.TargetValueField.Equals(value) != true)) {
                    this.TargetValueField = value;
                    this.RaisePropertyChanged("TargetValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TestCode {
            get {
                return this.TestCodeField;
            }
            set {
                if ((this.TestCodeField.Equals(value) != true)) {
                    this.TestCodeField = value;
                    this.RaisePropertyChanged("TestCode");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="QCConfigurationServiceReference.IQcConfigurationService")]
    public interface IQcConfigurationService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQcConfigurationService/GetQualityControl", ReplyAction="http://tempuri.org/IQcConfigurationService/GetQualityControlResponse")]
        QCEvaluation.WCFService.QCConfigurationServiceReference.GetQualityControlResponse GetQualityControl(QCEvaluation.WCFService.QCConfigurationServiceReference.GetQualityControlRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IQcConfigurationService/GetQualityControl", ReplyAction="http://tempuri.org/IQcConfigurationService/GetQualityControlResponse")]
        System.Threading.Tasks.Task<QCEvaluation.WCFService.QCConfigurationServiceReference.GetQualityControlResponse> GetQualityControlAsync(QCEvaluation.WCFService.QCConfigurationServiceReference.GetQualityControlRequest request);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IQcConfigurationServiceChannel : QCEvaluation.WCFService.QCConfigurationServiceReference.IQcConfigurationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class QcConfigurationServiceClient : System.ServiceModel.ClientBase<QCEvaluation.WCFService.QCConfigurationServiceReference.IQcConfigurationService>, QCEvaluation.WCFService.QCConfigurationServiceReference.IQcConfigurationService {
        
        public QcConfigurationServiceClient() {
        }
        
        public QcConfigurationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public QcConfigurationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public QcConfigurationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public QcConfigurationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public QCEvaluation.WCFService.QCConfigurationServiceReference.GetQualityControlResponse GetQualityControl(QCEvaluation.WCFService.QCConfigurationServiceReference.GetQualityControlRequest request) {
            return base.Channel.GetQualityControl(request);
        }
        
        public System.Threading.Tasks.Task<QCEvaluation.WCFService.QCConfigurationServiceReference.GetQualityControlResponse> GetQualityControlAsync(QCEvaluation.WCFService.QCConfigurationServiceReference.GetQualityControlRequest request) {
            return base.Channel.GetQualityControlAsync(request);
        }
    }
}
