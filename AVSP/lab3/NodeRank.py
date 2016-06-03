import sys

#MODE = 'LOCAL'
MODE = 'SPRUT'

FILE_PATH = "lab3A_primjeri/mtest2/R.in"


def main():
    number_of_nodes, follow_edge_chance, edges, nodes_for_results, iterations_for_results, node_degrees = parse_input(FILE_PATH)

    if MODE == 'LOCAL':
        print('Number of nodes: ', number_of_nodes)
        print('Chance to follow a node: ', follow_edge_chance)
        print('Size of edge dictionary:', len(edges))
        print('Size of iterations: ', len(iterations_for_results))

        print(edges)

    if MODE == 'LOCAL':
        print('Node degrees list constructed, len: ', len(node_degrees))

    output_string = ""
    for i, iteration_number in enumerate(iterations_for_results):
        rank = node_rank(iteration_number, nodes_for_results[i], edges, node_degrees, follow_edge_chance, number_of_nodes)
        output_string += '{0:.10f}'.format(rank) + '\n'

    print(output_string)


def parse_input(file_path):
    if MODE == 'LOCAL':
        file_handle = open(file_path, "r")
        lines = file_handle.read().split("\n")
    else:
        lines = sys.stdin.readlines()

    edges = {}
    iterations_for_results = []
    nodes_for_results = []
    node_degrees = {}
    for i, line in enumerate(lines):
        line_words = line.split(" ")

        if line == "":
            continue

        if i == 0:
            number_of_nodes = int(line_words[0])
            follow_edge_chance = float(line_words[1])
        elif 0 < i < 1 + number_of_nodes:
            for edge in line_words:
                if int(edge) in edges:
                    edges[int(edge)].append(i-1)
                else:
                    edges[int(edge)] = [i-1]



            node_degrees[i-1] = len(line_words)
        elif 1 + number_of_nodes <= i < 2 + number_of_nodes:
            number_of_results_needed = int(line_words[0])
        else:
            nodes_for_results.append(int(line_words[0]))
            iterations_for_results.append(int(line_words[1]))

    return number_of_nodes, follow_edge_chance, edges, nodes_for_results, iterations_for_results, node_degrees


def node_rank(number_of_iterations, node_index, edges, node_degrees, follow_edge_chance, number_of_nodes):
    ranks = [1.0/number_of_nodes] * number_of_nodes

    for i in range(0, number_of_iterations):
        S = 0.0
        ranks_next = [0.0] * number_of_nodes
        for j in range(0, number_of_nodes):
            if j not in edges:
                continue

            next_node_rank = follow_edge_chance * sum([(ranks[source_node]/node_degrees[source_node]) for source_node in edges[j]])
            ranks_next[j] += next_node_rank
            S += next_node_rank

        ranks = [rank + (1.0-S)/number_of_nodes for rank in ranks_next]

    return ranks[node_index]


if __name__ == '__main__':
    main()
