class PostsController < ApplicationController

	def home
		@posts = Post.all
	end
	
	def new
	end
	
	def create
		@post = Post.new(post_params)
		@post.save
	
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
