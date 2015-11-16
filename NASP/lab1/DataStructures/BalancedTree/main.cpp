#pragma once

#include <iostream>

#include "BalancedTree.h"

int main() {

	std::cout << "Creating a tree with the sequence: [1,2,3,4,5,6]" << std::endl;
	std::vector<int> input{ 3, 5, 2, 1, 6, 4 };

	BalancedTree tree = BalancedTree(input);
	tree.printTree(tree.getRoot());

	int stop;
	std::cin >> stop;

	return 1;
}
