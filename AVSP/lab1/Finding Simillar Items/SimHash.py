import hashlib
import binascii
import sys

MD5_LENGTH_BIT = 128
MD5_LENGTH_HEX = 32
HEX_SCALE = 16
BINARY_SCALE = 2
UTF_8 = 'utf-8'

MODE = 'SPRUT'
#MODE = 'LOCAL'

def main():
    (Q, N, corpus, I, K) = parse_input('input.txt')
    hashs = generate_signatures(corpus)
    #assert N == len(hashs)
    similarity_search(hashs, I, K, corpus)


def simhash(text):
    sh = [0] * MD5_LENGTH_BIT
    words = text.split(" ")[:-1]
    for word in words:
        bit_hash = hex_to_binary(hash_md5(word), MD5_LENGTH_BIT)
        #assert len(bit_hash) == MD5_LENGTH_BIT
        i = 0
        for bit in bit_hash:
            if bit == '1':
                sh[i] += 1
            else:
                sh[i] -= 1
            i += 1

    bit_sh_list = sh_to_bit(sh)
    bit_sh = ''.join(str(v) for v in bit_sh_list)  # Turn list into a string
    #assert len(bit_sh_list) == MD5_LENGTH_BIT
    return bit_sh


def hex_to_binary(hex, length):
    hex = bin(int('1'+str(hex.hexdigest()), HEX_SCALE))[3:]  # Don't remove leading zeroes
    while len(hex) < length:
        hex = '0' + hex

    return hex


def binary_to_hex(binary, length):
    binary = hex(int(binary, BINARY_SCALE))[2:]
    while len(binary) < length:
        binary = '0' + binary

    return binary


def sh_to_bit(final_sh):
    i = 0
    for bit in final_sh:
        if int(bit) >= 0:
            final_sh[i] = 1
        else:
            final_sh[i] = 0
        i += 1
    return final_sh


def hash_md5(word):
    return hashlib.md5(word.encode(UTF_8))


def parse_input(input):
    if MODE == 'LOCAL':
        f = open(input, "r")
        lines = f.read().split("\n")
    else:
        lines = sys.stdin.readlines()
    i = 0
    corpus = []
    I = []
    K = []
    Q = 0
    N = 0
    for line in lines:
        if line == '':
            continue;

        if i == 0:
            N = int(line)
        elif 0 < i <= N:
            corpus.append(line)
        elif i == (N + 1):
            Q = int(line)
        elif i > (N + 1):
            chunks = line.split(" ")
            I.append(int(chunks[0]))
            K.append(int(chunks[1]))

        i += 1

    return Q, N, corpus, I, K


def hem_distance(str1, str2):
    assert len(str1) == len(str2)
    diffs = 0
    for ch1, ch2 in zip(str1, str2):
            if ch1 != ch2:
                    diffs += 1

    return diffs


def generate_signatures(corpus):
    hashs = []
    for text in corpus:
        hashs.append(simhash(text))
    return hashs


def similarity_search(hashs, I, K, corpus):
    output_file = open("output.txt", "w+")
    output_string = ''

    for i in range(len(hashs)):
        number_of_matches = 0
        source_hash = hashs[I[i]]
        #assert source_hash == simhash(corpus[I[i]])
        goal_distance = K[i]
        j = -1
        for hash in hashs:
            j += 1
            if j == I[i]:   # Don't compare the has with itself
                continue

            if hem_distance(source_hash, hash) <= goal_distance:
                number_of_matches += 1

        output_string += str(number_of_matches) + '\n'

    if MODE == 'LOCAL':
        output_file.write(output_string)
    else:
        print(output_string)


if __name__ == '__main__':
    main()
