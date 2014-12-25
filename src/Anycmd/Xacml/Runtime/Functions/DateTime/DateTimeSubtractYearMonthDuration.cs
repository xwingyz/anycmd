using System;
using inf = Anycmd.Xacml.Interfaces;
using rtm = Anycmd.Xacml.Runtime;
using typ = Anycmd.Xacml.Runtime.DataTypes;

namespace Anycmd.Xacml.Runtime.Functions.DateTimeDataType
{
	/// <summary>
	/// Function implementation, in order to check the function behavior use the value of the Id
	/// property to search the function in the specification document.
	/// </summary>
	public class SubtractYearMonthDuration : FunctionBase
	{
		#region IFunction Members

		/// <summary>
		/// The id of the function, used only for notification.
		/// </summary>
		public override string Id
		{
			get{ return Consts.Schema1.InternalFunctions.DateTimeSubtractYearMonthDuration; }
		}

		/// <summary>
		/// Evaluates the function.
		/// </summary>
		/// <param name="context">The evaluation context instance.</param>
		/// <param name="args">The function arguments.</param>
		/// <returns>The result value of the function evaluation.</returns>
		public override Anycmd.Xacml.Runtime.EvaluationValue Evaluate( rtm.EvaluationContext context, params inf.IFunctionParameter[] args )
		{
			if (context == null) throw new ArgumentNullException("context");
			if (args == null) throw new ArgumentNullException("args");
			DateTime baseDate = GetDateTimeArgument( args, 0 );
			typ.YearMonthDuration yearMonthDuration = (typ.YearMonthDuration)args[1].GetTypedValue( DataTypeDescriptor.YearMonthDuration, 1 );

			baseDate = baseDate.AddYears( yearMonthDuration.Years*-1 );
			baseDate = baseDate.AddMonths( yearMonthDuration.Months*-1 );

			return new EvaluationValue( baseDate, DataTypeDescriptor.DateTime );
		}

		/// <summary>
		/// The data type of the return value.
		/// </summary>
		public override Anycmd.Xacml.Interfaces.IDataType Returns
		{
			get{ return DataTypeDescriptor.DateTime; }
		}

		/// <summary>
		/// Defines the data types for the function arguments.
		/// </summary>
		public override Anycmd.Xacml.Interfaces.IDataType[] Arguments
		{
			get
			{
				return new Anycmd.Xacml.Interfaces.IDataType[]{ DataTypeDescriptor.DateTime, DataTypeDescriptor.YearMonthDuration };
			}
		}
		#endregion
	}
}
