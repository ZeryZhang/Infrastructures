

namespace Hk.Infrastructures.Validator.TestHelper {
	using System;

	public class ValidationTestException : Exception {
		public ValidationTestException(string message) : base(message) {
		}
	}
}