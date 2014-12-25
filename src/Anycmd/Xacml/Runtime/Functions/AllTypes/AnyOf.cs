using System;

using inf = Anycmd.Xacml.Interfaces;
using rtm = Anycmd.Xacml.Runtime;

namespace Anycmd.Xacml.Runtime.Functions
{
    /// <summary>
    /// Function implementation, in order to check the function behavior use the value of the Id
    /// property to search the function in the specification document.
    /// </summary>
    public class AnyOf : FunctionBase
    {
        #region IFunction Members

        /// <summary>
        /// The id of the function, used only for notification.
        /// </summary>
        public override string Id
        {
            get { return Consts.Schema1.InternalFunctions.AnyOf; }
        }

        /// <summary>
        /// Method called by the EvaluationEngine to evaluate the function.
        /// </summary>
        /// <param name="context">The evaluation context instance.</param>
        /// <param name="args">The IFuctionParameters that will be used as arguments to the function.</param>
        /// <returns></returns>
        public override EvaluationValue Evaluate(rtm.EvaluationContext context, params inf.IFunctionParameter[] args)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (args == null) throw new ArgumentNullException("args");
            inf.IFunction function = args[0].GetFunction(0);
            EvaluationValue value = new EvaluationValue(args[1], args[1].GetType(context));
            foreach (object par in args[2].Elements)
            {
                EvaluationValue retVal = function.Evaluate(
                    context, value,
                    new EvaluationValue(par, args[2].GetType(context)));
                if (retVal.BoolValue)
                {
                    return retVal;
                }
            }
            return EvaluationValue.False;
        }

        /// <summary>
        /// The data type of the return value.
        /// </summary>
        public override inf.IDataType Returns
        {
            get { return DataTypeDescriptor.Boolean; }
        }

        /// <summary>
        /// Defines the data types for the function arguments.
        /// </summary>
        public override inf.IDataType[] Arguments
        {
            get
            {
                return new inf.IDataType[] { DataTypeDescriptor.Function, null, DataTypeDescriptor.Bag };
            }
        }

        #endregion
    }
}
