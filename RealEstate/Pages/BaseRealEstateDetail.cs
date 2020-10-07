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

        public string UserName { get; set; }

        public Comment NewComment { get; set; } = new Comment();

        public List<Comment> RealEstateComments { get; set; } = new List<Comment>();

        public CommentsPaging commentsPaging { get; set; } = new CommentsPaging();

        public int Totalpages { get; set; }

        public int currentPage = 1;

        public int QuantityPerPAge = 4;

        public async Task SelectedPage(int page)
        {
            currentPage = page;
            await LoadComments(page);
        }

        public async Task LoadComments(int page = 1, int quantityPerPage = 4)
        {

            try
            {
                RealEstateComments = await RealEstateService.GetRealEstateComments(Id, page, quantityPerPage);
            }
            catch(NullReferenceException n)
            {

            }
            catch(Exception e)
            {

            }
        }

        protected async override Task OnInitializedAsync()
        {
            NewComment.Errors = new List<string>();
            RealEstate.Urls = new List<URL>();
                RealEstate = (await RealEstateService.GetRealEstate(int.Parse(Id)));
            
            try
            {
                RealEstate.Urls.Add(new URL(RealEstate.ListingURL));
                UserName = RealEstate.UserName;

                Totalpages = (int)Math.Ceiling((decimal)await RealEstateService.GetTotalRealEstateComments(int.Parse(Id)) / QuantityPerPAge);
                await LoadComments();
            }
            catch(NullReferenceException n)
            {

            }
            catch(Exception e)
            {

            }
             
        }

        public async Task OnPostComment(PostedComment postedComment)
        {
            postedComment.RealEstateId = int.Parse(Id);
            NewComment = await RealEstateService.PostComment(postedComment);

            if(NewComment.IsSuccesfullCommentPost)
            {
                StateHasChanged();
            }
        }

    }
    
}
//TODO: Ladda om kommentarer efter postad kommentar
//TODO: Fixa CSS på sidan.
