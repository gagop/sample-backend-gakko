using System;

namespace GakkoBackend.Domain
{
    public partial class Candidate
    {
        public Guid IdCandidate { get; set; }
        public Guid IdCurriculum { get; set; }

        public virtual Person IdCandidateNavigation { get; set; }
        public virtual Curriculum IdCurriculumNavigation { get; set; }
    }
}
