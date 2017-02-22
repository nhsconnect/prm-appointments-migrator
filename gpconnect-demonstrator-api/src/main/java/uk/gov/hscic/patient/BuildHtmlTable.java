package uk.gov.hscic.patient;

import java.util.List;

public class BuildHtmlTable {

    private String tableBuilder(List<String> columnHeaders, List<List<Object>> list, String title) {
        // Creates the tables and the Section Headers
        StringBuilder buf = new StringBuilder("<h2>").append(title).append("</h2><table><thead><tr>");

        for (String columnHeader : columnHeaders) {
            buf.append("<th>").append(columnHeader).append("</th>");
        }

        buf.append("</tr></thead><tbody>");

        // Creates the rows
        for (int i = 0; i < list.size(); i++) {
            buf.append("<tr>");

            for (int j = 0; j < columnHeaders.size(); j++) {
                buf.append("<td>").append(list.get(i).get(j)).append("</td>");
            }
            buf.append("</tr>");
        }

        return buf.append("</tbody></table>").toString();
    }


    public String buildEmptyHtml(String sectionName) {
        return "<div><p>No " + sectionName + " data is recorded for this patient.</p></div>";
    }

    public String tableCreationFromObject(TableObject table) {

        table.getTitle();

        StringBuilder layout = new StringBuilder();
        String tables = tableBuilder(table.getHeaders(), table.getRows(), table.getTitle());
        layout.append(tables);
    
        return layout.toString();
    }


    public String addDiv(String htmlTable) {
        // TODO Auto-generated method stub
        StringBuilder sb = new StringBuilder("<div>").append(htmlTable).append("</div>");
        return sb.toString();
    }
}
