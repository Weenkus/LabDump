class Document < ActiveRecord::Base
  # It returns the articles whose titles contain one or more words that form the query
  def self.search(query)
    # where(:text, query) -> This would return an exact match of the query
    where("text like ?", "%#{query}%") 
  end
end
