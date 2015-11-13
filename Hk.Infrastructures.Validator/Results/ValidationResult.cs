

namespace Hk.Infrastructures.Validator.Results {
	using System;
	using System.Collections.Generic;
	using System.Linq;

#if !SILVERLIGHT && !PORTABLE && !PORTABLE40 && !CoreCLR
	[Serializable]
#endif
	public class ValidationResult {
		private readonly List<ValidationFailure> errors = new List<ValidationFailure>();

		public virtual bool IsValid {
			get { return Errors.Count == 0; }
		}

		public IList<ValidationFailure> Errors {
			get { return errors; }
		}

		public ValidationResult() {
		}

		public ValidationResult(IEnumerable<ValidationFailure> failures) {
			errors.AddRange(failures.Where(failure => failure != null));
		}
	}
}