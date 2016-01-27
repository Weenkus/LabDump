class Post
  include Mongoid::Document
  include Mongoid::Timestamps
  
  field :title, type: String
  field :content, type: String
  field :author, type: String
  field :image, type: String
  field :comments, type: Array
  
end
