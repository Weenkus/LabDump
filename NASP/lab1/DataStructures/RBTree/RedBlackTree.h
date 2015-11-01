#pragma once

#include <vector>

enum color { RED, BLACK};

struct node {
	int number;
	color color;
	node* left;
	node* right;

	node(int element, enum::color color)
	{
		// Constructor.  Make a node containing integer with a specific color (red or black).
		this->number = element;
		this->color = color;
		this->left = nullptr;
		this->right = nullptr;
	}
};

class RedBlackTree
{
public:
	RedBlackTree();
	~RedBlackTree();

	// Tree traversals
	void inorderTraversalPrint() const;

	// Data manipulation
	void insert(int element);

private:
	void insert(int element, node *leaf);
	void inorderTraversalPrint(node *root) const;

	node* root;
};

