using Microsoft.AspNetCore.Components;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Pages
{
    public class BaseRealEstateDetail : ComponentBase
    {

        public Propertys RealEstate { get; set; } = new Propertys();

        [Inject]
        public IRealEstateService RealEstateService { get; set; }

        [Parameter]
        public string Id { get; set; }

        public Comment NewComment { get; set; } = new Comment();

        public List<Comment> RealEstateComments { get; set; } = new List<Comment>();

        public CommentsPaging commentsPaging { get; set; } = new CommentsPaging();

        public int totalpages;

        public int currentPage = 1;

        public async Task SelectedPage(int page)
        {
            currentPage = page;
            await LoadComments(page);
        }

        public async Task LoadComments(int page = 1, int quantityPerPage = 4)
        {
            commentsPaging = await RealEstateService.GetRealEstateComments(Id, page, quantityPerPage);

            RealEstateComments = commentsPaging.Comments;
            totalpages = commentsPaging.TotalPages;
        }

        protected async override Task OnInitializedAsync()
        {
            NewComment.Errors = new List<string>();

            RealEstate.Urls = new List<string>();

            RealEstate = await RealEstateService.GetRealEstate(int.Parse(Id));
            RealEstate.Urls.Add(RealEstate.ListingURL);

            await LoadComments();
        }

        public async Task OnPostComment(PostedComment postedComment)
        {
            postedComment.RealEstateId = int.Parse(Id);
            NewComment = await RealEstateService.PostComment(postedComment);
        }

    }
    
}
