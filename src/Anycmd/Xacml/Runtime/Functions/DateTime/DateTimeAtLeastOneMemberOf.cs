

namespace Anycmd.Xacml.Runtime.Functions.DateTimeDataType
{
	/// <summary>
	/// Function implementation, in order to check the function behavior use the value of the Id
	/// property to search the function in the specification document.
	/// </summary>
	public class AtLeastOneMemberOf : BaseAtLeastOneMemberOf
	{
		#region IFunction Members

		/// <summary>
		/// The id of the function, used only for notification.
		/// </summary>
		public override string Id
		{
			get{ return Consts.Schema1.InternalFunctions.DateTimeAtLeastOneMemberOf; }
		}

		/// <summary>
		/// Defines the data type for which the function was defined for.
		/// </summary>
		public override Anycmd.Xacml.Interfaces.IDataType DataType
		{
			get{ return DataTypeDescriptor.DateTime; }
		}

		#endregion
	}
}
