namespace Application.DTOs.View
{
    public class GetRecommendationViewModel
    {
        public List<int> RecommendedUsers { get; set; }

        public GetRecommendationViewModel(List<int> recommendedUsers)
        {
            this.RecommendedUsers = recommendedUsers;
        }
    }
}