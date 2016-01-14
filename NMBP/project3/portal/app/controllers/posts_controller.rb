class PostsController < ApplicationController

	def home
		#@posts = Post.all
		
		# Get all cached posts
		@posts = Array.new
		sortedPOsts = LatestPost.order('created_at DESC').all # Sort
		sortedPOsts.each do |cache|
			if !cache.postId.nil?
				@posts << (Post.find(cache.postId))
			end
		end
	end
	
	def new
	end
	
	def create
		@post = Post.new(post_params)
		@post.save
		
		# Save the new post id in latestsPosts for fast retrieval
		latestId = LatestPost.new(postId:  Post.find_by(title: @post.title)[:id])
		
		# Check if the leatestPost documents are full (max 10)
		cachedNumberOfposts = 10
		if( LatestPost.count >= cachedNumberOfposts)
			# Remove the letest one and add the new one instead
			LatestPost.order('created_at ASC').limit(1).destroy
		end
		
		#Save the changes
		latestId.save
	
		redirect_to action: "home"
	end
	
	def comment
		@commentingPost = Post.find(params[:id])
	end
	
	def addComment
		@commentingPost = Post.find(params[:id])
		@commentingPost.add_to_set(comments: params[:comment])
		@commentingPost.save
	
		redirect_to action: "home"
	end
	
	private
		def post_params
		  params.require(:post).permit(:title, :image, :content, :author)
		end

end
