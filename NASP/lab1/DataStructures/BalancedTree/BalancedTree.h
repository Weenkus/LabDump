#pragma once

#include <vector>

struct node {
	int number;
	node* left;
	node* right;

	node(int element)
	{
		// Constructor.  Make a node containing integer.
		number = element;
		left = nullptr;
		right = nullptr;
	}
};

class BalancedTree
{
public:
	BalancedTree(std::vector<int>& input);
	~BalancedTree();

	// Getters
	node *getRoot() const { return root; }

	void printTree(node* root) const;
	void printTreePretty(node* root, int indent) const;

private:
	node* root = nullptr;

	// Main recursion that creates the tree
	void createTree(std::vector<int> input, node *&root);
};

	