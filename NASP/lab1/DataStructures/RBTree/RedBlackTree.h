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
	void inorderTraversalPrint(node* root) const;

	// Data manipulation
	void insert(node *&root, int element);

	// Getters
	node* getRoot() const { return this->root; }

private:
	node* root;
};

