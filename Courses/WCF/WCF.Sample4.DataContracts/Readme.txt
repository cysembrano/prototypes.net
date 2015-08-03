.NET automatically serializes all public properties of a complex type in alphabetical order.
Serializable attribute includes private fields in serialization.

DataContract and DataMember attributes allows for more control in serialization.  You can rename the complex type property.
You can change the order with which they are placed on the xsd. etc..  You can also add your own namespace on your complex types.