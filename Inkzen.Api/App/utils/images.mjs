/**
 * Description Return filename from URL
 *
 * @export
 * @param {string} url
 */
export function extractFilename(url) {
    const urlParts = url.split('/');
    const filename = urlParts.at(-1);
    const uuidDirectoryName = urlParts.at(-2);

    return `${uuidDirectoryName}-${filename}`;
}

export const ProjectImageWidth = 800;