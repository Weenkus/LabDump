#include "BalancedTree.h"

#include <iterator>
#include <algorithm>
#include <iostream>

BalancedTree::BalancedTree(std::vector<int>& input)
{
	// Print the contents for the input
	std::cout << "Input: ";
	for (const auto& i : input)
		std::cout << i << ' ';
	std::cout << std::endl;

	std::sort(input.begin(),input.end());

	// Print the sorted input
	std::cout << "Sorted input: ";
	for (const auto& i : input)
		std::cout << i << ' ';
	std::cout << std::endl;
	createTree(input, this->root);
}

BalancedTree::~BalancedTree()
{
}

void BalancedTree::printTree(node* root) const
{
	// Escape from the recursion
	if (root != nullptr)
	{
		std::cout << root->number << std::endl;
		printTree(root->left);
		printTree(root->right);
	}
}

void BalancedTree::printTreePretty(node * root, int indent) const
{
	// Escape from the recursion
	if (root != nullptr)
	{
		for (int i = 0; i < indent; i++) {
			std::cout << " ";
		}
		std::cout << root->number << std::endl;
		printTreePretty(root->left, indent - 4);
		printTreePretty(root->right, indent + 4);
		std::cout << std::endl;
	}
}

void BalancedTree::createTree(std::vector<int> input, node *&root) 
{
	// Exit recursion no more data
	if (input.size() == 0)
		return;

	// Create a new node
	int middleNumber = input.size() / 2;
	if (root == nullptr) {
		root = new node(input.at(middleNumber));
	}

	// Exit the recursion
	if (input.size() == 1)
		return;

	// Vector for the left branch
	std::vector<int>::iterator first = input.begin();
	std::vector<int>::iterator last = input.begin() + middleNumber;
	std::vector<int> leftInput(first, last);

	// Vector for the right branch
	first = input.begin() + middleNumber + 1;
	last = input.end();
	std::vector<int> rightInput(first, last);

	// Set the root number and start the recursion
	createTree(leftInput, root->left);
	createTree(rightInput, root->right);
}

